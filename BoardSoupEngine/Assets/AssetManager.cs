using BoardSoupEngine.Kernel;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace BoardSoupEngine.Assets
{
    internal class AssetManager : IEventListener, ITickable
    {
        private IEventDispatcher dispatcher;
        private Dictionary<String, Asset> assets;
        private bool hasAssetsWaitingForRenderer = false;

        public AssetManager()
        {
            assets = new Dictionary<String, Asset>();
        }

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;

            // if we have assets waiting for a renderer...
            if (hasAssetsWaitingForRenderer)
            {
                // for all the assets in our collection
                foreach (String s in assets.Keys)
                {
                    Asset a;
                    
                    assets.TryGetValue(s, out a);
                    // if the asset doesnt have a renderer
                    if (a.getRenderer() == null)
                    {
                        // make an event to request a renderer
                        AssetToRendererEvent atre = new AssetToRendererEvent();
                        atre.setAsset(a);

                        // send event
                        dispatcher.submitEvent(atre);
                    }
                }
            } 
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
        }

        public Asset loadAsset(AssetDetails argDetails, String argText, Point[] argPoints)
        {
            Asset a = null;

            // if an asset with this name already exists
            if (assets.ContainsKey( makeTextKey(argDetails, argText) ))
                assets.TryGetValue(argText, out a); // obtain it
            else
            {   // if not, make it and add it
                switch (argDetails.type)
                {
                    case AssetType.IMAGE:
                        a = makeImageAsset(argText);
                        break;
                    case AssetType.SHAPE:
                        a = makeShapeAsset(argText, argPoints);
                        break;
                    case AssetType.TEXT:
                        a = makeTextAsset(argText, argDetails);
                        break;
                }

                addAsset(a);
            }
            // return asset
            return a;
        }

        private String makeTextKey(AssetDetails argDetails, String argText)
        {
            if(argDetails is TextAssetDetails)
                return argText + ((TextAssetDetails)argDetails).fontSize + ((TextAssetDetails)argDetails).fontName + ((TextAssetDetails)argDetails).fontColor.ToString();

            return argText;
        }

        private Asset makeImageAsset(String filename)
        {
            return new ImageAsset(filename);
        }

        private Asset makeShapeAsset(String name, Point[] points)
        {
            return new ShapeAsset(name); 
        }

        private Asset makeTextAsset(String text, AssetDetails details)
        {
            if(details is TextAssetDetails)
            {
                TextAssetDetails tad = (TextAssetDetails)details;
                return new TextAsset(text, tad.fontName, tad.fontSize, tad.fontColor);
            }

            return new TextAsset(text);
        }

        private void addAsset(Asset a)
        {
            if (dispatcher != null)
            {
                // make an event to add a renderer to this asset
                AssetToRendererEvent atre = new AssetToRendererEvent();
                atre.setAsset(a);

                // send event
                dispatcher.submitEvent(atre);
            }
            else // we cannot send an event right away so we'll make sure this gets done once we have a dispatcher
                hasAssetsWaitingForRenderer = true;

            if(!assets.ContainsKey(a.getName()))
                assets.Add(a.getName(), a);
        }

    }
}
