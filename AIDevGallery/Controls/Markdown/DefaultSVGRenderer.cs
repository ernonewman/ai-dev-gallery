// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock;

internal class DefaultSVGRenderer : ISVGRenderer
{
    public async Task<Image> SvgToImage(string svgString)
    {
        SvgImageSource svgImageSource = new SvgImageSource();
        var image = new Image();

        // Create a MemoryStream object and write the SVG string to it
        using (var memoryStream = new MemoryStream())
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            await streamWriter.WriteAsync(svgString);
            await streamWriter.FlushAsync();

            // Rewind the MemoryStream
            memoryStream.Position = 0;

            // Load the SVG from the MemoryStream
            await svgImageSource.SetSourceAsync(memoryStream.AsRandomAccessStream());
        }

        // Set the Source property of the Image control to the SvgImageSource object
        image.Source = svgImageSource;
        var size = Extensions.GetSvgSize(svgString);
        if (size.Width != 0)
        {
            image.Width = size.Width;
        }

        if (size.Height != 0)
        {
            image.Height = size.Height;
        }

        return image;
    }
}