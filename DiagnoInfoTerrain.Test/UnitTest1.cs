using Diagnostique.BO.Controllers;
using Diagnostique.BO.Models;
using Diagnostique.BO.ModelSource;
using Microsoft.Extensions.Logging;
using Moq;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Diagnostique.Test
{
    public class DiagnoTerrainShould
    {
        [Fact]
        public void GetAllSourceEqualsNull()
        {
            // create mock logger object
            Mock<ILogger<DonneeSourceController>> mockloger1 = new Mock<ILogger<DonneeSourceController>>();
            // create mock dal object
            Mock<SourceDataAccessLayer> mockDalSource = new Mock<SourceDataAccessLayer>();

            var testUnitaire1 = new DonneeSourceController(mockloger1.Object, mockDalSource.Object);
            string testDate = DateTime.Now.ToShortDateString();
            var testing1 = testUnitaire1.SelectionDonneesSource(testDate, testDate, testDate, testDate, null, null, null);
            Assert.Null(testing1);
        }

        [Fact]
        public void GetAllSourceEqualObject()
        {
            SourceFormarteDonnees testItemOne = new SourceFormarteDonnees
            {
                ColId = 1,
                SourceFormarteUtilNom = "deiva",
                SourceFormarteUtilPrenom = "Rajaram",
                SourceFormarteSectNom = "COLOMIERS",
                SourceFormarteCatalPrincipal = "DP",
                SourceFormarteCatalAutrePrincipal = "Par défaut",
                SourceFormarteVersNom = "5.3.0.0",
                SourceFormarteComPremierDateId = 1,
                SourceFormarteComPremierDate = Convert.ToDateTime("2018-06-27 10:23:10.000"),
                SourceFormarteComDernierDateId = 2,
                SourceFormarteComDerniereDate = Convert.ToDateTime("2018-06-28 10:23:10.000"),
                SourceFormarteComPremierFactureDateId = 1,
                SourceFormarteComPremierFactureDate = Convert.ToDateTime("2018-06-25 10:23:10.000"),
                SourceFormarteComDernierFactureDateId = 2,
                SourceFormarteComDerniereFactureDate = Convert.ToDateTime("2018-06-26 10:23:10.000"),
                SourceFormarteVisDate = Convert.ToDateTime("2018-06-25 10:25:01.000"),
                SourceFormarteTabNom = new[] { "table1", "table2", "table3", "table4" },
                SourceFormarteTabLigneComptage = new[] { "1", "5", "6", "9" },
                SourceFormarteTabNomIndex = new List<IndexDetails> {
                    new IndexDetails { TableNom = "table1", Indexes = new[] { "index1", "index2" } },
                    new IndexDetails { TableNom = "table2", Indexes = new[] { "indexz", "indexw" } }
                }
            };

            // create mock 
            Mock<ILogger<DonneeSourceController>> mockloger2 = new Mock<ILogger<DonneeSourceController>>();
            // create mock dal object
            Mock<SourceDataAccessLayer> mockDalSource2 = new Mock<SourceDataAccessLayer>();

            var testUnitaire2 = new DonneeSourceController(mockloger2.Object, mockDalSource2.Object);
            string testDate2 = DateTime.MinValue.ToShortDateString();
            SourceFormarteDonnees testItemTwo = testUnitaire2.SelectionDonneesSource(testDate2, testDate2, testDate2, testDate2, null, null, null).FirstOrDefault();

            //Initialize objects and comparer           
            var comparer = new ObjectsComparer.Comparer<SourceFormarteDonnees>();

            //Compare objects
            var isEqual = comparer.Compare(testItemOne, testItemTwo, out IEnumerable<Difference> differences);

            Assert.True(isEqual);
        }

        //  https://www.codeproject.com/Articles/1212996/Using-Objects-Comparer-to-compare-complex-objects      

    }
}
