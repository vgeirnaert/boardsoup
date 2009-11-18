using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

namespace BoardSoupEngine.Utilities
{
    public class QuadTree<T> : IEnumerable
    {
        //=========================================
        // QuadTreeNode class
        //=========================================
        private class QuadTreeNode
        {
            enum QUAD { TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT };

            private List<T> payload;
            private bool isLeaf;
            private Dictionary<QUAD, QuadTreeNode> quads;
            private int bottomRightX;
            private int bottomRightY;
            private Rectangle dimensions;

            public QuadTreeNode(Rectangle argDimensions, int argLevel)
            {
                //Console.Write("new node, level " + argLevel + " details: " + argDimensions.ToString());
                // initialize
                payload = null;
                quads = null;
                isLeaf = false;
                bottomRightX = argDimensions.Right;
                bottomRightY = argDimensions.Bottom;
                dimensions = argDimensions;

                // if we are a leaf node (level 0)...
                if (argLevel == 0)
                    isLeaf = true;
                else // if we're not a leaf node, make children
                {
                    quads = new Dictionary<QUAD, QuadTreeNode>();

                    int leftWidth = (int)((float)dimensions.Width / 2);
                    int topHeight = (int)((float)dimensions.Height / 2);

                    quads.Add(QUAD.TOPLEFT, new QuadTreeNode(new Rectangle(dimensions.X, dimensions.Y, leftWidth, topHeight), argLevel - 1));
                    quads.Add(QUAD.TOPRIGHT, new QuadTreeNode(new Rectangle(dimensions.X + leftWidth, dimensions.Y, dimensions.Width - leftWidth, topHeight), argLevel - 1));
                    quads.Add(QUAD.BOTTOMLEFT, new QuadTreeNode(new Rectangle(dimensions.X, dimensions.Y + topHeight, leftWidth, dimensions.Height - topHeight), argLevel - 1));
                    quads.Add(QUAD.BOTTOMRIGHT, new QuadTreeNode(new Rectangle(dimensions.X + leftWidth, dimensions.Y + topHeight, dimensions.Width - leftWidth, dimensions.Height - topHeight), argLevel - 1));
                }
            }

            public bool add(T argT, Rectangle argRect)
            {
                // early out
                if (argT == null || argRect == null)
                   return false;

                if (isLeaf)
                {
                    if (payload == null)
                        payload = new List<T>();

                    payload.Add(argT);
                    return true;
                    
                }
                else
                {
                    bool added = false;

                    foreach (KeyValuePair<QUAD, bool> p in getQuadsForRect(argRect))
                    {
                        if (p.Value)
                        {
                            bool addresult = quads[p.Key].add(argT, argRect);
                            added = added || addresult;
                        }
                    }
                    return added;
                    
                }

                return false;
            }

            public List<T> getObjectsAt(Point argPoint)
            {
                if (isLeaf)
                    return payload;

                return quads[getQuadForPoint(argPoint)].getObjectsAt(argPoint);
            }

            public void clear()
            {
                payload = null;

                if (!isLeaf)
                {
                    foreach (QuadTreeNode q in quads.Values)
                        q.clear();
                }
            }

            public void remove(T argT, Rectangle argRect)
            {
                if (isLeaf)
                    payload.Remove(argT);
                else
                {
                    foreach (KeyValuePair<QUAD, bool> p in getQuadsForRect(argRect))
                    {
                        if (p.Value)
                            quads[p.Key].remove(argT, argRect);
                    }
                }
            }

            public Rectangle getRectangle()
            {
                return dimensions;
            }

            private QUAD getQuadForPoint(Point argP)
            {
                // top left: 0, top right: 1, bottom left: 2, bottom right: 3
                int square = 0;

                if (argP.X > (int)(dimensions.Width / 2) + dimensions.X)
                    square = square + 1;

                if (argP.Y > (int)(dimensions.Height / 2) + dimensions.Y)
                    square = square + 2;

                switch (square)
                {
                    case 0: return QUAD.TOPLEFT; 
                    case 1: return QUAD.TOPRIGHT; 
                    case 2: return QUAD.BOTTOMLEFT; 
                    case 3: return QUAD.BOTTOMRIGHT;
                }

                // we should never use this
                return QUAD.TOPLEFT;
            }

            private Dictionary<QUAD, bool> getQuadsForRect(Rectangle argR)
            {
                Dictionary<QUAD, bool> returnDict = new Dictionary<QUAD, bool>();
                //Console.WriteLine("checking rectangle: " + argR.ToString());
                //Console.WriteLine("against node dimension: " + dimensions.ToString());
                returnDict.Add(QUAD.TOPLEFT, false);
                returnDict.Add(QUAD.TOPRIGHT, false);
                returnDict.Add(QUAD.BOTTOMLEFT, false);
                returnDict.Add(QUAD.BOTTOMRIGHT, false);

                if (argR.IntersectsWith(quads[QUAD.TOPLEFT].getRectangle()))
                    returnDict[QUAD.TOPLEFT] = true;

                if (argR.IntersectsWith(quads[QUAD.TOPRIGHT].getRectangle()))
                    returnDict[QUAD.TOPRIGHT] = true;

                if (argR.IntersectsWith(quads[QUAD.BOTTOMLEFT].getRectangle()))
                    returnDict[QUAD.BOTTOMLEFT] = true;

                if (argR.IntersectsWith(quads[QUAD.BOTTOMRIGHT].getRectangle()))
                    returnDict[QUAD.BOTTOMRIGHT] = true;

                return returnDict;
            }
        }

        //==================================================
        // Actual quadtree class
        //==================================================
        private int levels = 4;
        private QuadTreeNode firstNode;
        private List<T> allObjects;

        public QuadTree(Rectangle argR)
        {
            firstNode = new QuadTreeNode(argR, levels);
            allObjects = new List<T>();
        }

        public void Add(T argT, Rectangle argRect)
        {
            if(firstNode.add(argT, argRect))
                allObjects.Add(argT);
        }

        public List<T> getObjectsAt(Point argPoint)
        {
            return firstNode.getObjectsAt(argPoint);
        }

        public void Clear()
        {
            firstNode.clear();
            allObjects.Clear();
        }

        public void Remove(T argT, Rectangle argRect)
        {
            firstNode.remove(argT, argRect);
            allObjects.Remove(argT);
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return allObjects.GetEnumerator();
        }

        #endregion
    }
}
