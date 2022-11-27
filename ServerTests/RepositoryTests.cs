using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace ServerTests
{
    // Setup code for in-memory database taken from Microsoft.com Learning center and associated Github repo
    // https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database
    // https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Testing/TestingWithoutTheDatabase/SqliteInMemoryBloggingControllerTest.cs
    [ExcludeFromCodeCoverage]
    public class RepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<DB> _contextOptions;

        #region SeedData
        private static readonly Disease d1 = new Disease { Id = 1, Name = "Common cold", Description = "Usually harmless viral infection of upper respiratory tract" };
        private static readonly Disease d2 = new Disease { Id = 2, Name = "Influenza", Description = "Viral infection of upper respiratory tract" };
        private static readonly Disease d3 = new Disease { Id = 3, Name = "Coronary heart disease", Description = "Common heart condition where the blood vessels struggle to supply the heart" };
        private static readonly Disease d4 = new Disease { Id = 4, Name = "Alzheimers disease", Description = "Progressive neurological disorder causing atrophy of the brain" };
        private static readonly Disease d5 = new Disease { Id = 5, Name = "Diabetes type 2", Description = "The impairment of the body to use sugar as fuel" };
        private static readonly Disease d6 = new Disease { Id = 6, Name = "Anemia", Description = "Caused by low red blood cell count" };
        private static readonly Disease d7 = new Disease { Id = 7, Name = "Covid", Description = "A respiratory disease carried by a coronavirus discovered in 2019" };

        private static readonly Symptom s1 = new Symptom { Id = 1, Name = "Runny nose" }; // Cold
        private static readonly Symptom s2 = new Symptom { Id = 2, Name = "Sneezing" }; // Cold
        private static readonly Symptom s3 = new Symptom { Id = 3, Name = "Coughing" }; // Cold and flu
        private static readonly Symptom s4 = new Symptom { Id = 4, Name = "High temperature" }; // Flu
        private static readonly Symptom s5 = new Symptom { Id = 5, Name = "Chest pain" };
        private static readonly Symptom s6 = new Symptom { Id = 6, Name = "Shortness of breath" };
        private static readonly Symptom s7 = new Symptom { Id = 7, Name = "Memory loss" };
        private static readonly Symptom s8 = new Symptom { Id = 8, Name = "Excessive hunger" };
        private static readonly Symptom s9 = new Symptom { Id = 9, Name = "Frequent urination" };
        private static readonly Symptom s10 = new Symptom { Id = 10, Name = "Fatigue" };
        private static readonly Symptom s11 = new Symptom { Id = 11, Name = "Loss of taste or smell" };

        private static readonly DiseaseSymptom ds1 = new DiseaseSymptom { Disease = d1, Symptom = s1 };
        private static readonly DiseaseSymptom ds2 = new DiseaseSymptom { Disease = d1, Symptom = s2 };
        private static readonly DiseaseSymptom ds3 = new DiseaseSymptom { Disease = d1, Symptom = s3 };
        private static readonly DiseaseSymptom ds4 = new DiseaseSymptom { Disease = d2, Symptom = s3 };
        private static readonly DiseaseSymptom ds5 = new DiseaseSymptom { Disease = d2, Symptom = s4 };
        private static readonly DiseaseSymptom ds6 = new DiseaseSymptom { Disease = d3, Symptom = s5 };
        private static readonly DiseaseSymptom ds7 = new DiseaseSymptom { Disease = d3, Symptom = s6 };
        private static readonly DiseaseSymptom ds8 = new DiseaseSymptom { Disease = d4, Symptom = s7 };
        private static readonly DiseaseSymptom ds9 = new DiseaseSymptom { Disease = d5, Symptom = s8 };
        private static readonly DiseaseSymptom ds10 = new DiseaseSymptom { Disease = d5, Symptom = s9 };
        private static readonly DiseaseSymptom ds11 = new DiseaseSymptom { Disease = d6, Symptom = s10 };
        private static readonly DiseaseSymptom ds12 = new DiseaseSymptom { Disease = d7, Symptom = s3 };
        private static readonly DiseaseSymptom ds13 = new DiseaseSymptom { Disease = d7, Symptom = s10 };
        private static readonly DiseaseSymptom ds14 = new DiseaseSymptom { Disease = d7, Symptom = s11 };
        private static readonly DiseaseSymptom ds15 = new DiseaseSymptom { Disease = d7, Symptom = s6 };

        private static string password = "Admin123";
        private static byte[] salt = ObligRepository.MakeSalt();
        private static byte[] hash = ObligRepository.MakeHash(password, salt);

        private static User user = new User { Username = "admin", Password = hash, Salt = salt };
        
        #endregion

        #region ConstructorAndDispose
        public RepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<DB>()
                .UseSqlite(_connection)
                .Options;

            using var context = new DB(_contextOptions);

            context.Users.Add(user);
            context.DiseaseSymptoms.AddRange(ds1, ds2, ds3, ds4, ds5, ds6, ds7, ds8, ds9, ds10, ds11, ds12, ds13, ds14, ds15);
            context.SaveChanges();
        }

        DB CreateContext()
        {
            return new DB(_contextOptions);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
        #endregion

        #region Tests
        [Fact]
        public async Task GetDisease_DiseaseExists()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var expected = new DiseaseDTO {
                Id = d1.Id,
                Name = d1.Name,
                Description = d1.Description,
                Symptoms = new string[] { s1.Name, s2.Name, s3.Name }
            };

            // Act
            var actual = await repository.GetDisease(1);

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(new HashSet<string>(expected.Symptoms), new HashSet<string>(actual.Symptoms));
        }

        [Fact]
        public async Task GetDisease_DiseaseDoesntExist()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.GetDisease(99);

            // Assert
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetAllDiseases_StringIsNullOrWhitespace(string searchString)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.GetAllDiseases(searchString);

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(i + 1, actual[i].Id);
            }
        }

        [Fact]
        public async Task GetAllDiseases_StringShouldReturnNoMatches()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.GetAllDiseases("aaaaaaabbbb");

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public async Task CreateDisease()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var disease = new Disease
            {
                Name = "Foo",
                Description = "Bar",
                DiseaseSymptoms = new List<DiseaseSymptom> { ds3, ds6, ds9 }
            };

            var expected = new DiseaseDTO
            {
                Id = 8,
                Name = "Foo",
                Description = "Bar",
                Symptoms = new string[] { ds3.Symptom.Name, ds6.Symptom.Name, ds9.Symptom.Name }
            };

            // Act
            var actual = await repository.CreateDisease(disease);

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(new HashSet<string>(expected.Symptoms), new HashSet<string>(actual.Symptoms));
        }

        [Fact]
        public async Task UpdateDisease()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var disease = new Disease
            {
                Id = 2,
                Name = "Foo",
                Description = "Bar",
                DiseaseSymptoms = new List<DiseaseSymptom> {
                    new DiseaseSymptom { DiseaseId = 2, SymptomId = 3 },
                    new DiseaseSymptom { DiseaseId = 2, SymptomId = 6 },
                    new DiseaseSymptom { DiseaseId = 2, SymptomId = 9 }
                }
            };

            // Act
            var created = await repository.UpdateDisease(disease);
            var actual = await context.Diseases.FindAsync(2);

            // Assert
            Assert.True(created);
            Assert.Equal(disease.Id, actual.Id);
            Assert.Equal(disease.Name, actual.Name);
            Assert.Equal(disease.Description, actual.Description);
            Assert.Equal(disease.DiseaseSymptoms, actual.DiseaseSymptoms);
        }

        [Fact]
        public async Task UpdateDisease_DiseaseSymptomsNotSupplied()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var disease = new Disease
            {
                Id = 2,
                Name = "Foo",
                Description = "Bar",
                DiseaseSymptoms = new List<DiseaseSymptom>()
            };

            // Act
            var created = await repository.UpdateDisease(disease);
            var actual = await context.Diseases.FindAsync(2);

            // Assert
            Assert.True(created);
            Assert.Equal(disease.Id, actual.Id);
            Assert.Equal(disease.Name, actual.Name);
            Assert.Equal(disease.Description, actual.Description);
            Assert.Empty(actual.DiseaseSymptoms);
            Assert.IsType<List<DiseaseSymptom>>(actual.DiseaseSymptoms);
        }

        [Fact]
        public async Task DeleteDisease()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.DeleteDisease(1);
            var deleted = await context.Diseases.FindAsync(1);

            // Assert
            Assert.True(actual);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task DeleteDisease_IdNotFound()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.DeleteDisease(99);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task GetAllSymptoms()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.GetAllSymptoms();

            // Assert
            Assert.IsType<List<SymptomDTO>>(actual);
            for (int i = 0; i < 11; i++)
            {
                Assert.Equal(i + 1, actual[i].Id);
            }
        }

        [Theory]
        [InlineData(5, 1, 1, 5)]
        [InlineData(5, 2, 6, 10)]
        [InlineData(5, 3, 11, 11)]
        [InlineData(5, -1, 1, 5)]
        [InlineData(5, 999, 11, 11)]
        [InlineData(10, 1, 1, 10)]
        [InlineData(10, 2, 11, 11)]
        [InlineData(10, -1, 1, 10)]
        [InlineData(10, 999, 11, 11)]
        [InlineData(20, 1, 1, 11)]
        [InlineData(20, -1, 1, 11)]
        [InlineData(20, 999, 1, 11)]
        public async Task GetSymptomsTable_DifferentPageSizesAndPageNumbers(
            int pageSize, int pageNumber, int firstId, int lastId)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var options = new SymptomsTableOptions
            {
                OrderByOptions = "idAscending",
                SearchString = "",
                SelectedSymptoms = new List<SymptomDTO>(),
                PageSize = pageSize,
                PageNum = pageNumber
            };

            // Act
            var actual = await repository.GetSymptomsTable(options);

            // Assert
            Assert.IsType<SymptomsTable>(actual);
            Assert.IsType<List<SymptomDTO>>(actual.SymptomList);
            for (int i = firstId, j = 0; i <= lastId; i++, j++)
            {
                Assert.Equal(i, actual.SymptomList[j].Id);
            }
        }

        [Theory]
        [InlineData("idAscending", new int[] { 1, 2, 3, 4, 5 })]
        [InlineData("idDescending", new int[] {11, 10, 9, 8, 7})]
        [InlineData("nameAscending", new int[] {5, 3, 8, 10, 9})]
        [InlineData("nameDescending", new int[] {2, 6, 1, 7, 11})]
        [InlineData("", new int[] {1, 2, 3, 4, 5})]
        [InlineData("   ", new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(null, new int[] { 1, 2, 3, 4, 5 })]
        public async Task GetSymptomsTable_SortingOptions(string orderByOptions, int[] ids)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var options = new SymptomsTableOptions
            {
                OrderByOptions = orderByOptions,
                SearchString = "",
                SelectedSymptoms = new List<SymptomDTO>(),
                PageSize = 5,
                PageNum = 1
            };

            // Act
            var actual = await repository.GetSymptomsTable(options);

            // Assert
            Assert.IsType<SymptomsTable>(actual);
            Assert.IsType<List<SymptomDTO>>(actual.SymptomList);
            for (int i = 0; i < options.PageSize; i++)
            {
                Assert.Equal(ids[i], actual.SymptomList[i].Id);
            }
        }

        [Theory]
        [InlineData("Loss", 2)]
        [InlineData("es", 3)]
        [InlineData("aaaaaaabbbbbb", 0)]
        public async Task GetSymptomsTable_searchTerms(string searchString, int count)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var options = new SymptomsTableOptions
            {
                OrderByOptions = "idAscending",
                SearchString = searchString,
                SelectedSymptoms = new List<SymptomDTO>(),
                PageSize = 5,
                PageNum = 1
            };

            // Act
            var actual = await repository.GetSymptomsTable(options);

            // Assert
            Assert.IsType<SymptomsTable>(actual);
            Assert.IsType<List<SymptomDTO>>(actual.SymptomList);
            Assert.Equal(count, actual.SymptomList.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task GetSymptomsTable_NullOrEmptySearchString(string searchString)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var options = new SymptomsTableOptions
            {
                OrderByOptions = "idAscending",
                SearchString = searchString,
                SelectedSymptoms = new List<SymptomDTO>(),
                PageSize = 5,
                PageNum = 1
            };

            // Act
            var actual = await repository.GetSymptomsTable(options);

            // Assert
            Assert.IsType<SymptomsTable>(actual);
            Assert.IsType<List<SymptomDTO>>(actual.SymptomList);
            Assert.Equal(options.PageSize, actual.SymptomList.Count);
        }
        [Fact]
        public async Task GetSymptomsTable_ExcludeSymptoms()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var options = new SymptomsTableOptions
            {
                OrderByOptions = "idAscending",
                SearchString = "",
                SelectedSymptoms = new List<SymptomDTO>() { 
                    new SymptomDTO { Id = 1 },
                    new SymptomDTO { Id = 2 },
                    new SymptomDTO { Id = 3 }
                },
                PageSize = 20,
                PageNum = 1
            };

            // Act
            var actual = await repository.GetSymptomsTable(options);

            // Assert
            Assert.IsType<SymptomsTable>(actual);
            Assert.IsType<List<SymptomDTO>>(actual.SymptomList);
            var ids = new int[] { 1, 2, 3 };
            for (int i = 0; i < actual.PageData.NumEntries; i++)
            {
                Assert.DoesNotContain<int>(actual.SymptomList[i].Id, ids);
            }
        }

        [Fact]
        public async Task GetRelatedSymptoms()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            // Act
            var actual = await repository.GetRelatedSymptoms(7);
            var expected = new int[] { 3, 10, 11, 6 };

            // Assert
            Assert.IsType<List<SymptomDTO>>(actual);
            Assert.Equal(expected.Length, actual.Count);
            foreach (var item in actual)
            {
                Assert.Contains(item.Id, expected);
            }
        }

        [Fact]
        public async Task SearchDiseases_OneSymptom()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var selectedSymptoms = new List<Symptom> {
                s3
            };
            
            var expected = new int[] { 1, 2, 7 };

            // Act
            var actual = await repository.SearchDiseases(selectedSymptoms);

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            Assert.Equal(expected.Length, actual.Count);
            foreach (var item in actual)
            {
                Assert.Contains(item.Id, expected);
            }
        }

        [Fact]
        public async Task SearchDiseases_TwoSymptoms()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var selectedSymptoms = new List<Symptom> {
                s3, s11
            };

            var expected = 7;

            // Act
            var actual = await repository.SearchDiseases(selectedSymptoms);

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            Assert.Single(actual);
            Assert.Equal(expected, actual[0].Id);
        }

        [Fact]
        public async Task SearchDiseases_NoMatches()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var selectedSymptoms = new List<Symptom> {
                s1, s2, s3, s4, s5, s6
            };

            // Act
            var actual = await repository.SearchDiseases(selectedSymptoms);

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public async Task SearchDiseases_NoSymptomsSelected()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var selectedSymptoms = new List<Symptom>();

            // Act
            var actual = await repository.SearchDiseases(selectedSymptoms);

            // Assert
            Assert.IsType<List<DiseaseDTO>>(actual);
            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("admin", "Admin123", true)]
        [InlineData("foo", "bar", false)]
        public async Task LogIn(string username, string password, bool loggedIn)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ObligRepository(context);

            var user = new UserDTO { Username = username, Password = password };

            // Act
            var actual = await repository.LogIn(user);

            // Assert
            Assert.Equal(loggedIn, actual);
        }
        #endregion
    }
}
