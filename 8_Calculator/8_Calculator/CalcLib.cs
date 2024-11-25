﻿using _8_Calculator.DB.Entities;
using _8_Calculator.DTOs;
using _8_Calculator.Kafka;
using Confluent.Kafka;
using System.Text.Json;

namespace _8_Calculator
{
    public class CalcLib
    {
        private readonly DatabaseContext _context;
        private readonly KafkaProducerService<Null, string> _producer;

        public CalcLib(DatabaseContext context, KafkaProducerService<Null, string> producer)
        {
            _context = context;
            _producer = producer;
        }

        public void Delete(int id)
        {
            var calculation = _context.Calculations.Find(id);

            if (calculation != null)
            {
                _context.Calculations.Remove(calculation);
                _context.SaveChangesAsync();
            }
            return;
        }

        public string Calculate(double num1, double num2, string operation)
        {
            var res = GetResult(num1,num2,operation);

            SendDataToKafka(res);

            _context.Calculations.Add(res);
            _context.SaveChanges();
            return res.Result.ToString();
        }

        private void SendDataToKafka(Calculation res)
        {
            var data = new CalcDTO
            {
                Operand1 = res.Operand1,
                Operand2 = res.Operand2,
                Operation = res.Operation,
                Result = res.Result,
            };
            var json = JsonSerializer.Serialize(data);
            _producer.Produce("KorotkovYuA", new Message<Null, string> { Value = json });
        }

        public static Calculation GetResult(double num1, double num2, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "Add":
                    result = num1 + num2;
                    break;
                case "Subtract":
                    result = num1 - num2;
                    break;
                case "Multiply":
                    result = num1 * num2;
                    break;
                case "Divide":
                    result = num1 / num2;
                    break;
            }
            var calc = new Calculation
            {
                Operand1 = num1,
                Operand2 = num2,
                Result = result,
                Operation = operation.ToString()
            };
            return calc;
        }
    }
}