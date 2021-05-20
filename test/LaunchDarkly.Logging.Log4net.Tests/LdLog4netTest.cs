using System;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using Xunit;

namespace LaunchDarkly.Logging.Tests
{
    public class LdLog4netTest
    {
        [Fact]
        public void TestAdapter()
        {
            var log4netCapture = new MemoryAppender();
            BasicConfigurator.Configure(log4netCapture);

            var ourAdapter = LdLog4net.Adapter;
            var logger1 = ourAdapter.Logger("things");
            var logger2 = logger1.SubLogger("stuff");

            logger1.Debug("d0");
            logger1.Debug("d1,{0}", "a");
            logger1.Debug("d2,{0},{1}", "a", "b");
            logger1.Debug("d3,{0},{1},{2}", "a", "b", "c");
            logger1.Info("i0");
            logger1.Info("i1,{0}", "a");
            logger1.Info("i2,{0},{1}", "a", "b");
            logger1.Info("i3,{0},{1},{2}", "a", "b", "c");
            logger1.Warn("w0");
            logger1.Warn("w1,{0}", "a");
            logger1.Warn("w2,{0},{1}", "a", "b");
            logger1.Warn("w3,{0},{1},{2}", "a", "b", "c");
            logger1.Error("e0");
            logger1.Error("e1,{0}", "a");
            logger1.Error("e2,{0},{1}", "a", "b");
            logger1.Error("e3,{0},{1},{2}", "a", "b", "c");
            logger2.Warn("goodbye");

            Assert.Collection(log4netCapture.GetEvents(),
                ExpectEvent("things", Level.Debug, "d0"),
                ExpectEvent("things", Level.Debug, "d1,a"),
                ExpectEvent("things", Level.Debug, "d2,a,b"),
                ExpectEvent("things", Level.Debug, "d3,a,b,c"),
                ExpectEvent("things", Level.Info, "i0"),
                ExpectEvent("things", Level.Info, "i1,a"),
                ExpectEvent("things", Level.Info, "i2,a,b"),
                ExpectEvent("things", Level.Info, "i3,a,b,c"),
                ExpectEvent("things", Level.Warn, "w0"),
                ExpectEvent("things", Level.Warn, "w1,a"),
                ExpectEvent("things", Level.Warn, "w2,a,b"),
                ExpectEvent("things", Level.Warn, "w3,a,b,c"),
                ExpectEvent("things", Level.Error, "e0"),
                ExpectEvent("things", Level.Error, "e1,a"),
                ExpectEvent("things", Level.Error, "e2,a,b"),
                ExpectEvent("things", Level.Error, "e3,a,b,c"),
                ExpectEvent("things.stuff", Level.Warn, "goodbye")
                );
        }

        private Action<LoggingEvent> ExpectEvent(string loggerName,
            Level level, string message)
        {
            return e =>
            {
                Assert.Equal(level, e.Level);
                Assert.Equal(loggerName, e.LoggerName);
                Assert.Equal(message, e.RenderedMessage);
            };
        }
    }
}
