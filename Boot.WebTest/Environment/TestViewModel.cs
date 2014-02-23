
using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions;

namespace Boot.WebTest.Environment
{
    /// <summary>
    /// TestViewModel
    /// </summary>
    public class TestViewModel
    {
        public string ResultFromFirstDb { get; set; }
        public string ResultFromSecondDb { get; set; }
        public string FooterText { get; set; }

        public List<ModelTest> AllInModelTestOne { get; set; }
        public ModelTest ModelTestTwo { get; set; }

        public TestViewModel()
        {
            AllInModelTestOne = SessionFactory
                 .With<ModelTest>("First") //Open connection First
                     .OpenSession()
                        .All<ModelTest>();

            ModelTestTwo = SessionFactory
                .With<ModelTest>("Second") //Open connection Second
                    .OpenSession()
                       .Find<ModelTest>(m => m.Id == 1)
                            .FirstOrDefault();

            //...

            ResultFromFirstDb = AllInModelTestOne.Find(t => t.Id == 3).Text;
            ResultFromSecondDb = ModelTestTwo.Text;
            FooterText = AllInModelTestOne.Find(t=> t.Id == 2).Text;
        }
    }
}