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
                // initialize
                payload = null;
                quads = null;
                bottomRightX = argDimensions.Right;
                bottomRightY = argDimensions.Bottom;
                dimensions = argDimensions;

                // if we are a leaf node (level 0)...
                if (argLevel == 0)
                    isLeaf = true;
                else // if we're not a leaf node, make children
                {
                    quads = new Dictionary<QUAD, QuadTreeNode>();

                    int leftWidth = (int)(bottomRightX / 2);
                    int topHeight = (int)(bottomRightY / 2);

                    quads.Add(QUAD.TOPLEFT, new QuadTreeNode(new Rectangle(dimensions.X, dimensions.Y, leftWidth, topHeight), argLevel - 1));
                    quads.Add(QUAD.TOPRIGHT, new QuadTreeNode(new Rectangle(dimensions.X + leftWidth, dimensions.Y, dimensions.Width - leftWidth, topHeight), argLevel - 1));
                    quads.Add(QUAD.BOTTOMLEFT, new QuadTreeNode(new Rectangle(dimensions.X, dimensions.Y + topHeight, leftWidth, dimensions.Height - topHeight), argLevel - 1));
                    quads.Add(QUAD.BOTTOMRIGHT, new QuadTreeNode(new Rectangle(dimensions.X + leftWidth, dimensions.Y + topHeight, dimensions.Width - leftWidth, dimensions.Height - topHeight), argLevel - 1));
                }
            }

            public void add(T argT, Rectangle argRect)
            {
                // early out
                if (argT == null || argRect == null)
                    return;

                if (isLeaf)
                {
                    if (payload == null)
                        payload = new List<T>();

                    payload.Add(argT);
                }
                else
                {
                    foreach (KeyValuePair<QUAD, bool> p in getQuadsForRect(argRect))
                    {
                        if(p.Value)
                            quads[p.Key].add(argT, argRect);
                    }
                    
                }
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

            public void remove(T argT, Point argPoint)
            {
                if (isLeaf)
                    payload.Remove(argT);
                else
                    quads[getQuadForPoint(argPoint)].remove(argT, argPoint);
            }

            public Rectangle getRectangle()
            {
                return dimensions;
            }

            private QUAD getQuadForPoint(Point argP)
            {
                // top left: 0, top right: 1, bottom left: 2, bottom right: 3
                int square = 0;

                if (argP.X > (int)(bottomRightX / 2))
                    square = square + 1;

                if (argP.Y > (int)(bottomRightY / 2))
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
        private int levels = 5;
        private QuadTreeNode firstNode;
        private List<T> allObjects;

        public QuadTree(Rectangle argR)
        {
            firstNode = new QuadTreeNode(argR, levels);
            allObjects = new List<T>();
        }

        public void Add(T argT, Rectangle argRect)
        {
            firstNode.add(argT, argRect);
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

        public void Remove(T argT, Point argPoint)
        {
            firstNode.remove(argT, argPoint);
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
