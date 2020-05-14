using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships_V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_V2.Tests
{
    [TestClass()]
    public class PlayerTests

    {
        Ship s1 = new Ship(Type.UBoot);
        Ship s2 = new Ship(Type.Zerstörer);
        Ship s3 = new Ship(Type.Zerstörer);
        Ship s4 = new Ship(Type.Kreuzer);
        Ship s5 = new Ship(Type.Schlachtschiff);
        Ship s6 = new Ship(Type.Flugzeugtraeger);
        
        Player p1 = new Player("test", true);


        [TestMethod()]
        public void Ckeckplaceable_Test_1()
        {
            Assert.IsFalse(p1.Check_placeable(s1,10,10));
        }

        [TestMethod()]
        public void Ckeckplaceable_Test_2()
        {
            Assert.IsFalse(p1.Check_placeable(s2, 9, 9));
        }

        [TestMethod()]
        public void Ckeckplaceable_Test_3()
        {
            Assert.IsTrue(p1.Check_placeable(s1, 5, 5));
        }

        [TestMethod()]
        public void Ckeck_Place()
        {
            s4.orientation = Orientation.vertical;
            s4.Y = 1;
            s4.X = 9;
            p1.place(s4);
            Assert.AreEqual(s4, p1.board[1,9].ship);
            Assert.AreEqual(s4, p1.board[2,9].ship);
            Assert.AreEqual(s4, p1.board[3,9].ship);
        }
        [TestMethod()]
        public void Ckeck_Place_1()
        {
            s6.Y = 1;
            s6.X = 3;
            p1.place(s6);
            Assert.AreEqual(s6, p1.board[1, 3].ship);
            Assert.AreEqual(s6, p1.board[1, 4].ship);
            Assert.AreEqual(s6, p1.board[1, 5].ship);
            Assert.AreEqual(s6, p1.board[1, 6].ship);
            Assert.AreEqual(s6, p1.board[1, 7].ship);

        }
        [TestMethod()]
        public void Ckeck_Place_2()
        {
            s2.Y = 5;
            s2.X = 5;
            s2.orientation = Orientation.vertical;
            p1.place(s2);
            Assert.AreEqual(s2, p1.board[5, 5].ship);
            Assert.AreEqual(s2, p1.board[6, 5].ship);

        }
        [TestMethod()]
        public void Ckeck_Placeships()
        {
            p1.placeships();
            int is_s1= 0;
            int is_s2= 0;
            int is_s3= 0;
            int is_s4= 0;
            int is_s5= 0;
            int is_s6= 0;
            for(int i= 0; i <= 9; i++) 
            {
                for (int u = 0; u <= 9; u++)
                {
                    if (p1.board[i, u].ship == p1.ships[0])
                        is_s1++;
                    if (p1.board[i, u].ship == p1.ships[1])
                        is_s2++;
                    if (p1.board[i, u].ship == p1.ships[2])
                        is_s3++;
                    if (p1.board[i, u].ship == p1.ships[3])
                        is_s4++;
                    if (p1.board[i, u].ship == p1.ships[4])
                        is_s5++;
                    if (p1.board[i, u].ship == p1.ships[5])
                        is_s6++;
                }
            }
            Assert.AreEqual(1, is_s1);
            Assert.AreEqual(2, is_s2);
            Assert.AreEqual(2, is_s3);
            Assert.AreEqual(3, is_s4);
            Assert.AreEqual(4, is_s5);
            Assert.AreEqual(5, is_s6);        
        }
        [TestMethod()]
        public void Ckeck_fire_at()
        {
            s2.Y = 5;
            s2.X = 5;
            s2.orientation = Orientation.vertical;
            p1.place(s2);
            p1.fire_at(5, 5);
            p1.fire_at(5,6);
            Assert.AreEqual(Status.sunk , p1.board[5, 5].status);
            Assert.AreEqual(Status.sunk ,p1.board[6, 5].status);

        }
        [TestMethod()]
        public void Ckeck_fire_at_1()
        {
            s6.Y = 5;
            s6.X = 9;
            s6.orientation = Orientation.vertical;
            p1.place(s6);
            p1.fire_at(9, 5);
            p1.fire_at(9, 6);
            Assert.AreEqual(Status.hit, p1.board[5, 9].status);
            Assert.AreEqual(Status.hit, p1.board[6, 9].status);

        }
        [TestMethod()]
        public void Ckeck_fire_at_2()
        {
            s6.Y = 5;
            s6.X = 9;
            s6.orientation = Orientation.vertical;
            p1.place(s6);
            p1.fire_at(9, 5);
            p1.fire_at(9, 6);
            p1.fire_at(9, 7);
            p1.fire_at(9, 8);
            p1.fire_at(9, 9);
            Assert.AreEqual(Status.sunk, p1.board[5, 9].status);
            Assert.AreEqual(Status.sunk, p1.board[6, 9].status);

        }
        [TestMethod()]
        public void Ckeck_fire_at_3()
        {
            p1.fire_at(9, 5);
            p1.fire_at(9, 6);
            Assert.AreEqual(Status.miss, p1.board[5, 9].status);
            Assert.AreEqual(Status.miss, p1.board[6, 9].status);

        }
    }
}