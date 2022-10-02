using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ControllerTest
{
    [TestFixture]
    internal class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track tracktest = new Track("test", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.Finish});
            _competition.Tracks.Enqueue(tracktest);
            var result = _competition.NextTrack();
            Assert.AreEqual (tracktest, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track tracktest = new Track("test", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.Finish});
            _competition.Tracks.Enqueue(tracktest);
            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track tracktest = new Track("test", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.Finish});
            Track tracktest2 = new Track("test2", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Finish});
            _competition.Tracks.Enqueue(tracktest);
            _competition.Tracks.Enqueue(tracktest2);
            var result = _competition.NextTrack();
            Assert.IsTrue(result == tracktest);
            var result2 = _competition.NextTrack();
            Assert.IsTrue(result2 == tracktest2);



        }




    }
}
