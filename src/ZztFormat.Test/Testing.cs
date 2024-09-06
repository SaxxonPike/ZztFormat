using NUnit.Framework.Internal;

namespace ZztFormat.Test;

public static class Testing
{
    public static Randomizer Random => TestContext.CurrentContext.Random;
}