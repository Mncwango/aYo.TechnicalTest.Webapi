using aYo.TechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aYo.TechnicalTest.Services
{
    public interface IConversionService
    {
        Task<double> Converstion(int inputA, ConversionType conversionType, ConversionUnits conversionUnits);
    }
}
