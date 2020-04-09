﻿using bibliothek.Models;
using Microsoft.Extensions.Logging;
using Nager.AmazonProductAdvertising;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace bibliothek.Contracts
{
    public class AmazonEnhanceMedia : IEnhanceMedia
    {
        private readonly ILogger<AmazonEnhanceMedia> _logger;

        public AmazonEnhanceMedia(ILogger<AmazonEnhanceMedia> logger)
        {
            this._logger = logger;
        }

        public Tuple<string, List<SimilarBooks>> GetDetails(string isbn)
        {
            try
            {
                var accessKey = ConfigurationManager.AppSettings["AmazonAccessKey"];
                var secretKey = ConfigurationManager.AppSettings["AmazonSecretKey"];
                var associateTag = ConfigurationManager.AppSettings["AmazonAssociateTag"];

                var authentication = new AmazonAuthentication();
                authentication.AccessKey = accessKey;
                authentication.SecretKey = secretKey;

                var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.DE, associateTag);
                var result = wrapper.Lookup(isbn);

                var item = result?.Items?.Item?.FirstOrDefault();

                var asins = item?.SimilarProducts?.Select(o => o.ASIN).ToList();
                if (asins == null)
                {
                    return new Tuple<string, List<SimilarBooks>>(item?.LargeImage?.URL, null);
                }

                var similarBooksRequest = wrapper.Lookup(asins);
                if (similarBooksRequest == null)
                {
                    Thread.Sleep(1000);
                    similarBooksRequest = wrapper.Lookup(asins);
                }

                var similarBooks = similarBooksRequest?.Items?.Item?.Select(o => new SimilarBooks()
                {
                    Isbn = o.ItemAttributes.EAN,
                    Title = o.ItemAttributes.Title,
                    Verfasser = o.ItemAttributes.Author?.FirstOrDefault(),
                    ImageUrl = o.MediumImage?.URL,
                    Url = o.DetailPageURL
                }).ToList();

                return new Tuple<string, List<SimilarBooks>>(item?.LargeImage?.URL, similarBooks);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, $"Cannot get details for {isbn}");
                return null;
            }
        }
    }
}