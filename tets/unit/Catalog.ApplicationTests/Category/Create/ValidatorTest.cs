using Catalog.Application.Category.Create;
using Catalog.Application.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.ApplicationTests.Category.Create
{
    public class ValidatorTests
    {
        [Theory]
        [ClassData(typeof(Create_Category_validatorTestData))]
        public async Task Create_Category_Validator(CreateCategoryDto model, bool isvalid)
        {
            using (var ctx = await TestDbContext.Create(nameof(Create_Category_Validator)))
            {


                var vaidator = new Catalog.Application.Category.Create.Validator(ctx);
                Assert.False(vaidator.Validate(new CreateCategory(null)).IsValid);

                Assert.Equal(isvalid, vaidator.Validate(new CreateCategory(model)).IsValid);

            }
        }
        public class Create_Category_validatorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new CreateCategoryDto{ Name = null}, false };
                yield return new object[] { new CreateCategoryDto { Name = "" }, false };


            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
