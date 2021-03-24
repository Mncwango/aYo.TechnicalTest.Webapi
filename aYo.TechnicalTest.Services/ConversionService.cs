using AutoMapper;
using aYo.TechnicalTest.Data.UnitofWork;
using aYo.TechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aYo.TechnicalTest.Services
{
    public class ConversionService : IConversionService
    {
        protected IUnitOfWork _unitOfWork;
        //protected IMapper _mapper;
        public ConversionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<double> Converstion(int inputA, ConversionType conversionType, ConversionUnits conversionUnits)
        {
            switch (conversionType)
            {
                case ConversionType.Temperature:
                    {
                        if (conversionUnits == ConversionUnits.Celsius)
                        {
                            var Output = (inputA * 9/5)  + 32;
                            await this.Audit(inputA, Output.ToString(), conversionType, conversionUnits);
                            return Output;
                        }
                        else
                        {
                            var Output = (inputA - 32) * 5 / 9;
                            await this.Audit(inputA, Output.ToString(), conversionType, conversionUnits);
                            return Output;
                        }
                    }
                case ConversionType.Length:
                    {
                        if (conversionUnits == ConversionUnits.Meter)
                        {
                            var output = (inputA / 100);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Meter);
                            return output;
                        }
                        else
                        {
                            var output = (inputA * 100);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Centimeter);
                            return output;
                        }
                    }
                case ConversionType.Mass:
                    {
                        if (conversionUnits == ConversionUnits.Kilogram)
                        {
                            var output = (inputA * 1000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Kilogram);
                            return output;
                        }
                        else
                        {
                            var output = (inputA / 1000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Gram);
                            return output;
                        }
                    }
                case ConversionType.Pressure:
                    {
                        if (conversionUnits == ConversionUnits.Pascal)
                        {
                            var output = (inputA / 100000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Pascal);
                            return output;
                        }
                        else
                        {
                            var output = (inputA * 100000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Bar);
                            return output;
                        }
                    }
                case ConversionType.Volume:
                    {
                        if (conversionUnits == ConversionUnits.Liter)
                        {
                            var output = (inputA * 1000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Liter);
                            return output;
                        }
                        else
                        {
                            var output = (inputA / 1000);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.Mililiter);
                            return output;
                        }
                    }
                case ConversionType.Speed:
                    {
                        if (conversionUnits == ConversionUnits.KiloMeterPerHour)
                        {
                            var output = (inputA / 3.6);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.KiloMeterPerHour);
                            return output;
                        }
                        else
                        {
                            var output = (inputA * 3.6);
                            await Audit(inputA, output.ToString(), conversionType, ConversionUnits.MeterPerSecond);
                            return output;
                        }
                    }
                default:
                    return 0;

            }
        }


        private async Task<int> Audit(int inputA, string output, ConversionType conversionType, ConversionUnits conversionUnits)
        {
            await this._unitOfWork.GetRepository<ConversionAuditLog>().Insert(new ConversionAuditLog
            {
                ConversionType = conversionType,
                Inputs = inputA.ToString(),
                Output = output,
                ConversionUnits = conversionUnits.ToString()
            });
            return await this._unitOfWork.SaveAsync();

        }
    }
}
