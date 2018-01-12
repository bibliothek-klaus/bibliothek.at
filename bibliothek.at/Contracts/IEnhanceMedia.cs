using bibliothek.at.Models;
using System;
using System.Collections.Generic;

namespace bibliothek.at.Contracts
{
    public interface IEnhanceMedia
    {
        Tuple<string, List<SimilarBooks>> GetDetails(string isbn);
    }
}