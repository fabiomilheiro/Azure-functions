
using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.Primitives;

namespace Azure_functions
{
    public static class GenerateThumbnail
    {
        [FunctionName("GenerateThumbnail")]
        public static void Run(
            [BlobTrigger("profiles/{path}", Connection = "Storage")]Stream originalStream,
            string path,
            ILogger log,
            [Blob("profile-thumbnails/{path}", FileAccess.Write, Connection = "Storage")]Stream thumbnailStream)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:profiles/{path} \n Size: {originalStream.Length} Bytes");

            using (var image = Image.Load(originalStream))
            {
                image.Mutate(context => context
                    .ApplyProcessor(
                        new ResizeProcessor(
                            new ResizeOptions
                            {
                                Size = new Size(50, 50)
                            },
                            new Size(image.Width, image.Height))));
                image.Save(thumbnailStream, PngFormat.Instance);
            }
        }
    }
}
