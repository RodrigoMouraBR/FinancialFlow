using Bogus;
using Bogus.DataSets;
using Carrefas.FinancialFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefas.FinancialFlow.Tests.HumanData
{
    [CollectionDefinition(nameof(CarrefasBogusCollection))]
    public class CarrefasBogusCollection : ICollectionFixture<CarrefasBogusCollection>
    {
    }

    public class CarrefasBogusFixture
    {
        public FinancialPosting GenerateFinancialPostingValid()
        {
            return Generate(1).FirstOrDefault();
        }

        public IEnumerable<FinancialPosting> Generate(int quantidade)
        {            
            var userFinancialPostingFaker = new Faker<FinancialPosting>("pt_BR")
                .CustomInstantiator(f => new FinancialPosting(f.Finance.Amount(0, 1000, 2), f.Date.Recent().ToUniversalTime(), true));

            return userFinancialPostingFaker.Generate(quantidade);
        }
    }
}
