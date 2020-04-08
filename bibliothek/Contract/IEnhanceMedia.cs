using bibliothek.Models;
using System;
using System.Collections.Generic;

namespace bibliothek.Contracts
{
    public interface IEnhanceMedia
    {
        Tuple<string, List<SimilarBooks>> GetDetails(string isbn);
    }
}