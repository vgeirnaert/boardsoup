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

        public Asset loadAsset(String filename)
        {
            Asset ia;

            // if an asset with this name already exists
            if (assets.ContainsKey(filename))
                assets.TryGetValue(filename, out ia); // obtain it
            else
            {   // if not, make it and add it
                ia = new ImageAsset(filename);
                addAsset(ia);
            }
            // return asset
            return ia;
        }

        public Asset loadAsset(String name, Point[] points)
        {
            Asset sa;

            // if an asset with this name already exists
            if (assets.ContainsKey(name))
                assets.TryGetValue(name, out sa); // obtain it
            else
            {   // if not, make it and add it
                sa = new ShapeAsset(name);
                addAsset(sa);
            }
            // return asset
            return sa;
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

            assets.Add(a.getName(), a);
        }

    }
}
