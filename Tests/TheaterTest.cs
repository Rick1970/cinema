using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class TheaterTest : IDisposable
  {
    public TheaterTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Theater.DeleteAll();
      Movie.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Theater.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfTheaterIsSame()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater firstTheater = new Theater("Regal", fakeTime);
      Theater secondTheater = new Theater("Regal",fakeTime);

      Assert.Equal(firstTheater, secondTheater);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      List<Theater> result = Theater.GetAll();
      List<Theater> testList = new List<Theater>{testTheater};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToTheater()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      Theater savedTheater = Theater.GetAll()[0];
      int result = savedTheater.GetId();
      int testId = testTheater.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsTheaterInDatabase()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      Theater foundTheater = Theater.Find(testTheater.GetId());

      Assert.Equal(testTheater, foundTheater);
    }
  }
}
