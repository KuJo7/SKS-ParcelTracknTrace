using FizzWare.NBuilder;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.DataAccess.Sql;

namespace TeamJ.SKS.Package.DataAccess.Test
{
    public class SqlHopRepositoryTest
    {
        private IHopRepository hop_repo;
        private List<DALHop> hops;
        private List<DALWarehouse> warehouses;
        //private List<DALWarehouseNextHops> warehousesNextHops;
        private List<DALTruck> trucks;
        //private List<DALTransferwarehouse> transferWarehouses;


        [SetUp]
        public void Setup()
        {


            var warehouse = Builder<DALWarehouse>.CreateNew()
                .With(x => x.Code = "1234")
                .With(x => x.HopType = "Warehouse")
                .With(x => x.Level = 0)
                .With(x => x.NextHops = new List<DALWarehouseNextHops>{
                    Builder<DALWarehouseNextHops>.CreateNew()
                        .With(x => x.Hop = Builder<DALHop>.CreateNew()
                        .Build())
                        .Build()
                })
                .Build();

            var truck = Builder<DALHop>.CreateNew()
                .With(x => x.Code = "1234")
                .With(x => x.HopType = "Truck")
                .Build();

            hops = new List<DALHop>();
            hops.Add(warehouse);
            hops.Add(truck);

            warehouses = new List<DALWarehouse> { hops[0] as DALWarehouse };

            trucks = new List<DALTruck> { hops[1] as DALTruck };

            var mockDbContext = new Mock<IContext>();
            mockDbContext.Setup(p => p.Hops).Returns(DbContextMock.GetQueryableMockDbSet<DALHop>(hops));
            mockDbContext.Setup(p => p.Parcels).Returns(DbContextMock.GetQueryableMockDbSet<DALParcel>(new List<DALParcel>()));
            //mockDbContext.Setup(p => p.Recipients).Returns(DbContextMock.GetQueryableMockDbSet<DALRecipient>(new List<DALRecipient>()));
            var mockLogger = new Mock<ILogger<SqlHopRepository>>();
            hop_repo = new SqlHopRepository(mockDbContext.Object, mockLogger.Object);


        }

        [Test]
        public void Create_Hop_Success()
        {
            var hop = new DALHop();
            hop_repo.Create(hop);
            Assert.AreEqual(3, hops.Count);
            Assert.AreEqual(hop, hops[2]);
        }

        [Test]
        public void Update_Success()
        {
            hops[0].Description = "new Description";
            Assert.DoesNotThrow(() => hop_repo.Update(hops[0]));
        }

        [Test]
        public void Delete_Success()
        {
            var hop = new DALHop();
            hop_repo.Delete(hop);
            Assert.AreEqual(2, hops.Count);
        }

        [Test]
        public void Delete_Failed()
        {
            var hop = new DALHop();
            hop_repo.Delete(hop);
            Assert.AreEqual(2, hops.Count);
        }

        [Test]
        public void GetHopByHopType_Success()
        {
            var hop = hop_repo.GetByHopType("Warehouse");
            Assert.AreEqual(hops[0].HopType, "Warehouse");
        }

        [Test]
        public void GetHopByHopType_Failed()
        {
            var hop = hop_repo.GetByHopType("wrongHopType");
            Assert.AreEqual(hops[0].HopType, "Warehouse");
        }

        [Test]
        public void GetHopByCode_Success()
        {
            var hop = hop_repo.GetByCode("1234");
            Assert.AreEqual(hops[0].Code, hop.Code);
        }

        [Test]
        public void GetHopByCode_Failed()
        {
            Assert.Throws<DataAccessException>(() => hop_repo.GetByCode("wrongCode"));   
        }

        [Test]
        public void GetAllHops_Success()
        {
            //var wh = hop_repo.GetAllHops();
            Assert.AreEqual(warehouses, warehouses.ToList());
        }

        [Test]
        public void GetFirstHop_Success()
        {
            var root = hop_repo.GetFirstHop();
            Assert.AreEqual(hops[0].Code, root.Code);
        }
    }
}
