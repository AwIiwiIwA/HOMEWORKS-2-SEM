using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NetworkConfiguration.Tests
{
    [TestFixture]
    public class NetworkConfigurationTests
    {
        private List<Router> _routers = new List<Router>();

        [SetUp]
        public void Setup()
        {
            _routers = new List<Router>();
        }

        [Test]
        public void LoadGraph_EmptyFile_NoRoutersLoaded()
        {
            // Arrange
            string filePath = "C:\\Users\\Aorus\\source\\repos\\router\\file.txt";

            // Act
            Program.LoadGraph(filePath);

            // Assert
            Assert.That(_routers, Is.Empty);
        }

        [Test]
        public void IsGraphConnected_EmptyGraph_ReturnsFalse()
        {
            // Act
            bool isConnected = Program.IsGraphConnected();

            // Assert
            Assert.IsFalse(isConnected);
        }

        [Test]
        public void IsGraphConnected_OneRouter_ReturnsTrue()
        {
            // Arrange
            _routers.Add(new Router { Id = 1 });

            // Act
            bool isConnected = Program.IsGraphConnected();

            // Assert
            Assert.IsTrue(isConnected);
        }

        [Test]
        public void KruskalAlgorithm_EmptyGraph_ReturnsEmptyList()
        {
            // Act
            List<Edge> result = Program.KruskalAlgorithm();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void KruskalAlgorithm_OneRouter_ReturnsEmptyList()
        {
            // Arrange
            _routers.Add(new Router { Id = 1 });

            // Act
            List<Edge> result = Program.KruskalAlgorithm();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void KruskalAlgorithm_ThreeRouters_ReturnsTwoEdges()
        {
            // Arrange
            var router1 = new Router { Id = 1 };
            var router2 = new Router { Id = 2 };
            var router3 = new Router { Id = 3 };

            router1.Edges.Add(new Edge { From = router1, To = router2, Weight = 10 });
            router1.Edges.Add(new Edge { From = router1, To = router3, Weight = 5 });
            router2.Edges.Add(new Edge { From = router2, To = router1, Weight = 10 });
            router2.Edges.Add(new Edge { From = router2, To = router3, Weight = 1 });
            router3.Edges.Add(new Edge { From = router3, To = router1, Weight = 5 });
            router3.Edges.Add(new Edge { From = router3, To = router2, Weight = 1 });

            _routers.Add(router1);
            _routers.Add(router2);
            _routers.Add(router3);

            // Act
            List<Edge> result = Program.KruskalAlgorithm();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Sum(edge => edge.Weight), Is.EqualTo(15));
        }
    }
}
