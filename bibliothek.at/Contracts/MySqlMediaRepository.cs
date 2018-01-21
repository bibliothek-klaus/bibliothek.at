using bibliothek.at.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace bibliothek.at.Contracts
{
    public class MySqlMediaRepository : IMediaRepository
    {
        private string _connectionString;

        public MySqlMediaRepository()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            this._connectionString = connectionString;
        }

        public Dictionary<string, string> GetMediaTypes()
        {
            var medienArt = new Dictionary<string, string>();
            medienArt.Add("D", "Dichtung");
            medienArt.Add("K", "Kinderbücher");
            medienArt.Add("J", "Jugendbücher");
            medienArt.Add("S", "Sachbücher");
            medienArt.Add("W", "Kinderhörbücher");
            //medienArt.Add("1", "Filme");
            //medienArt.Add("2", "Filme");
            medienArt.Add("3", "Hörbücher");
            medienArt.Add("4", "Jugendhörbücher");
            return medienArt;
        }

        public MediaStatus GetMediaStatus()
        {
            var item = new MediaStatus();

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    return item;
                }

                using (var command = new MySqlCommand("SELECT Medienart, COUNT(*) AS count FROM Medien GROUP BY Medienart", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var medienart = reader["Medienart"] as string;
                            var count = Convert.ToInt32(reader["count"]);

                            switch (medienart)
                            {
                                case "1":
                                    item.MovieCount += count;
                                    break;
                                case "2":
                                    item.MovieCount += count;
                                    break;
                                case "3":
                                    item.AudioBookCount += count;
                                    break;
                                case "4":
                                    item.AudioBookCount += count;
                                    break;
                                case "W":
                                    item.AudioBookCount += count;
                                    break;
                                case "J":
                                    item.BookCount += count;
                                    break;
                                case "S":
                                    item.BookCount += count;
                                    break;
                                case "D":
                                    item.BookCount += count;
                                    break;
                                case "K":
                                    item.BookCount += count;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return item;
        }

        private MediaItem ReadMediaItem(MySqlDataReader reader)
        {
            var borrowed = false;
            var status = reader["Status"] as string;
            if (status != null)
            {
                if (status.Equals("entlehnt") || status.Equals("verlängert"))
                {
                    borrowed = true;
                }
            }

            var item = new MediaItem();
            item.Status = borrowed;
            item.Id = (int)reader["Mediennummer"];
            item.MedienArt = reader["Medienart"] as string;
            item.Sachtitel = reader["Sachtitel"] as string;
            item.Titelzusatz = reader["Titelzusatz"] as string;
            item.Systematik = reader["Systematik"] as string;
            item.Verfasser = reader["Verfasser_1"] as string;
            item.ISBN = reader["ISBN"] as string;
            item.Rezension = reader["Rezension"] as string;
            item.Verlag = reader["Verlag"] as string;
            item.Entlehnungen = (int)reader["Entlehnungen"];

            try
            {
                item.PurchaseDate = (DateTime)reader["Einstelldatum"];
            }
            catch
            {
                item.PurchaseDate = new DateTime(2000, 1, 1);
            }

            return item;
        }

        public List<MediaItem> GetMediaItems(string search)
        {
            var items = new List<MediaItem>();

            var checkInput = new Regex("^[a-z0-9äöüß -]*$", RegexOptions.IgnoreCase);
            if (!checkInput.IsMatch(search))
            {
                return items;
            }

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    var query = new StringBuilder();

                    int id;
                    if (int.TryParse(search, out id))
                    {
                        query.AppendFormat(" Mediennummer = {0}", id);
                    }
                    else
                    {
                        var parts = search.Split(' ');
                        //mehrere wörter nach denen gesucht werden soll
                        if (parts.Length > 1)
                        {
                            foreach (var part in parts)
                            {
                                if (part.ToLower().Trim().Equals("dvd"))
                                {
                                    query.Append(" AND Medienart = '1' ");
                                    continue;
                                }
                                query.AppendFormat(" AND (Sachtitel LIKE '%{0}%' OR Verfasser_1 LIKE '%{0}%' OR Systematik LIKE '%{0}%')", part.Trim());
                            }
                        }
                        //ein wort nach dem gesucht werden soll
                        else
                        {
                            var part = parts.FirstOrDefault();
                            if (part.Length <= 3)
                            {
                                query.AppendFormat(" AND Systematik LIKE '{0}%'", part.Trim());
                            }
                            else
                            {
                                query.AppendFormat(" AND (Sachtitel LIKE '%{0}%' OR Verfasser_1 LIKE '%{0}%' OR Systematik LIKE '{0}%')", part.Trim());
                            }
                        }
                        query.Remove(0, 4);
                    }
                    
                    query.Insert(0, "SELECT * FROM Medien WHERE");
                    //query.Append(" LIMIT 1000");

                    using (var command = new MySqlCommand(query.ToString(), connection))
                    {
                        //% = Wildcard
                        //command.Parameters.Add("search", MySqlDbType.VarChar).Value = search + "%";
                        //command.Parameters.Add("search", MySqlDbType.VarChar).Value = "%" + search + "%";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = this.ReadMediaItem(reader);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

            return items;
        }

        public List<MediaItem> GetMediaItemsByMediaType(string mediaType)
        {
            var items = new List<MediaItem>();

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT * FROM Medien WHERE Medienart = @Medienart", connection))
                    {
                        var filterDate = DateTime.Now.AddDays(-20);
                        command.Parameters.Add("Medienart", MySqlDbType.VarChar, 50).Value = mediaType;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = this.ReadMediaItem(reader);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return items;
        }

        public List<MediaItem> GetNewMediaItems()
        {
            var items = new List<MediaItem>();

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT * FROM Medien WHERE Medienart IN ('J', 'D', 'S', 'K', 'W', '3', '4') AND Einstelldatum > @FilterDate ORDER BY Einstelldatum DESC", connection))
                    {
                        var filterDate = DateTime.Now.AddDays(-20);
                        command.Parameters.Add("FilterDate", MySqlDbType.DateTime).Value = filterDate;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = this.ReadMediaItem(reader);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return items;
        }

        public List<MediaItem> GetPopularMediaItems()
        {
            var items = new List<MediaItem>();

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT * FROM Medien WHERE ISBN IS NOT NULL AND ISBN <> '' ORDER BY Entlehnungen DESC LIMIT 500", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = this.ReadMediaItem(reader);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

            return items.GroupBy(o => o.MedienArt).Select(o => new { o.Key, Items = o.OrderByDescending(x => x.Entlehnungen).Take(10) }).SelectMany(o => o.Items).ToList();
        }

        public MediaItem GetMediaItem(int id)
        {
            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT * FROM Medien WHERE Mediennummer = @Id", connection))
                    {
                        var filterDate = DateTime.Now.AddDays(-20);
                        command.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = this.ReadMediaItem(reader);
                                return item;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

            return null;
        }

        public List<AvailableMediaItem> CheckIsbnsAvailable(List<string> isbns)
        {
            var items = new List<AvailableMediaItem>();

            using (var connection = new MySqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    var sb = new StringBuilder();
                    foreach (var isbn in isbns)
                    {
                        sb.Append($",'{isbn}'");
                    }
                    sb.Remove(0, 1);

                    using (var command = new MySqlCommand($"SELECT Mediennummer, ISBN FROM Medien WHERE ISBN IN ({sb.ToString()})", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new AvailableMediaItem
                                {
                                    Id = (int)reader["Mediennummer"],
                                    ISBN = reader["ISBN"] as string
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

           return items;
        }
    }
}