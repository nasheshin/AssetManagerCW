using System;
using System.Linq;
using AssetManager.DataUtils;
using AssetManager.Models;
using NUnit.Framework;

namespace DataProcessorTests
{
    [TestFixture]
    public class FunctionsTests
    {
        private readonly DataProcessorFactory _processorFactory;

        public FunctionsTests()
        {
            var database = new DataContext();
            _processorFactory = new DataProcessorFactory(database);
        }

        [Test]
        public void FindAnalyticsByName_HasAnalytic_ReturnAnalytic()
        {
            var proc = (DataProcessorAnalytics) _processorFactory.CreateProcessor(DataProcessorType.AssetAnalytics);
            
            var analytic = proc.FindAnalyticByName("ПермЭнергоСбыт");
            
            Assert.IsNotNull(analytic);
        }
        
        [Test]
        public void FindAnalyticsByName_HasNotAnalytic_ReturnNull()
        {
            var proc = (DataProcessorAnalytics) _processorFactory.CreateProcessor(DataProcessorType.AssetAnalytics);
            
            var analytic = proc.FindAnalyticByName("NoExistAnalytic");
            
            Assert.IsNull(analytic);
        }
        
        [Test]
        public void FindBrokerByName_HasBroker_ReturnBroker()
        {
            var proc = (DataProcessorBrokers) _processorFactory.CreateProcessor(DataProcessorType.Brokers);
            
            var analytic = proc.FindBrokerByName("Тинькофф");
            
            Assert.IsNotNull(analytic);
        }
        
        [Test]
        public void FindBrokerByName_HasNotBroker_ReturnNull()
        {
            var proc = (DataProcessorBrokers) _processorFactory.CreateProcessor(DataProcessorType.Brokers);
            
            var analytic = proc.FindBrokerByName("NoExistBroker");
            
            Assert.IsNull(analytic);
        }
        
        [Test]
        public void AddBroker_ElementIsNotBroker_ThrowException()
        {
            var proc = (DataProcessorBrokers) _processorFactory.CreateProcessor(DataProcessorType.Brokers);

            try
            {
                proc.AddElement(new Operation());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddBroker_ElementIsBroker_AddedBroker()
        {
            var proc = (DataProcessorBrokers) _processorFactory.CreateProcessor(DataProcessorType.Brokers);
            const string name = "TestName";
            
            proc.AddElement(new Broker{Name = name});

            Assert.IsTrue(name == proc.Brokers.LastOrDefault()?.Name);
        }
        
        [Test]
        public void AddLog_ElementIsNotLog_ThrowException()
        {
            var proc = (DataProcessorLogs) _processorFactory.CreateProcessor(DataProcessorType.Logs, 1);

            try
            {
                proc.AddElement(new Operation());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddLog_ElementIsLog_AddedLog()
        {
            var proc = (DataProcessorLogs) _processorFactory.CreateProcessor(DataProcessorType.Logs, 1);
            const string message = "TestName";
            
            proc.AddElement(new Log{ Message = message, Datetime = DateTime.Today});

            var lastLog = proc.Logs.LastOrDefault();
            Assert.IsNotNull(lastLog);
            Assert.AreEqual(message, lastLog.Message);
        }
        
        [Test]
        public void AddOperation_ElementIsNotOperation_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.AddElement(new Log());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddOperation_OperationUserIdAreNotTheSame_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.AddElement(new Operation {UserId = 2});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddOperation_OperationTickerNotCorrect_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.AddElement(new Operation {AssetTicker = "aaaaaa"});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddOperation_OperationDatetimeNotCorrect_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.AddElement(new Operation {Datetime = DateTime.MaxValue});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddOperation_ElementIsCorrect_AddedOperationAndGotEvent()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);
            var eventRaised = false;
            proc.DatabaseChanged += (s, e) => {eventRaised = true;};
            var operation = new Operation
            {
                AssetAnalyticId = 0, AssetName = "name", AssetTicker = "AAAA", AssetType = "type",
                BrokerId = 0, Datetime = new DateTime(2020, 12, 12), Price = 100, Type = 1, UserId = 1
            };

            proc.AddElement(operation);

            var lastOperation = proc.Operations.LastOrDefault();
            if (lastOperation == null)
                Assert.Fail();
            
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(operation.AssetAnalyticId, lastOperation.AssetAnalyticId);
            Assert.AreEqual(operation.AssetName, lastOperation.AssetName);
            Assert.AreEqual(operation.AssetTicker, lastOperation.AssetTicker);
            Assert.AreEqual(operation.AssetType, lastOperation.AssetType);
            Assert.AreEqual(operation.BrokerId, lastOperation.BrokerId);
            Assert.AreEqual(operation.Datetime, lastOperation.Datetime);
            Assert.AreEqual(operation.Price, lastOperation.Price);
            Assert.AreEqual(operation.Type, lastOperation.Type);
            Assert.AreEqual(operation.UserId, lastOperation.UserId);
        }
        
        [Test]
        public void RemoveOperation_ElementIsNotOperation_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.RemoveElement(new Log());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void RemoveOperation_OperationUserIdAreNotTheSame_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.RemoveElement(new Operation {UserId = 2});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void RemoveOperation_NoOperationInDatabase_ThrowException()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);

            try
            {
                proc.RemoveElement(new Operation {Id = -100});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void RemoveOperation_ElementIsCorrect_RemovedOperationAndGotEvent()
        {
            var proc = (DataProcessorOperations) _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);
            var eventRaised = false;
            proc.DatabaseChanged += (s, e) => {eventRaised = true;};
            var operation = new Operation { Id = 1, UserId = 1};

            proc.RemoveElement(operation);

            var foundOperation = proc.Operations.FirstOrDefault(op => op.Id == 1);

            Assert.IsTrue(eventRaised);
            Assert.IsNull(foundOperation);
        }
        
        [Test]
        public void AddUser_ElementIsNotUser_ThrowException()
        {
            var proc = (DataProcessorUsers) _processorFactory.CreateProcessor(DataProcessorType.Users, 1);

            try
            {
                proc.AddElement(new Log());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddUser_NameNotCorrect_ThrowException()
        {
            var proc = (DataProcessorUsers) _processorFactory.CreateProcessor(DataProcessorType.Users, 1);

            try
            {
                proc.AddElement(new User {Name = "NotCorrectLogin|||||||3131fddddd!!!!", Password = "CorrectPassword"});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddUser_PasswordNotCorrect_ThrowException()
        {
            var proc = (DataProcessorUsers) _processorFactory.CreateProcessor(DataProcessorType.Users, 1);

            try
            {
                proc.AddElement(new User {Name = "CorrectLogin", Password = "NotCorrectPassword|||||||3131fddddd"});
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void AddUser_ElementIsCorrect_AddedUser()
        {
            var proc = (DataProcessorUsers) _processorFactory.CreateProcessor(DataProcessorType.Users, 1);
            var user = new User { Name = "CorrectLogin", Password = "CorrectPassword"};

            proc.AddElement(user);

            var lastUser = proc.Users.LastOrDefault();
            Assert.IsNotNull(lastUser);
            Assert.AreEqual(user.Name, lastUser.Name);
            Assert.AreEqual(user.Password, lastUser.Password);
        }
    }
}