namespace CRUDTests {

    public class UnitTest1 {

        [Fact]
        public void Test1() {
            //Arrange
            int input1 = 2;
            int input2 = 3;
            int expected = 5;
            MyMath myMath = new MyMath();
            //Act
            int answer = myMath.Add(input1, input2);
            //Assert
            Assert.Equal(expected, answer);
        }
    }
}