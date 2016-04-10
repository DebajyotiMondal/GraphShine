using System.Collections.Generic;
using GraphShine.GeometricPrimitives;

namespace GraphShine.Utilities
{
    public class NearestNeighbor
    {
        


        public static Dictionary<int, int> L1NearestNeighborInCone(Point[] P, int ConeId)
        {           
            
            Rectangle rect = Point.GetBoundary(P);
            double maxX = rect.TopRightCorner.x;
            double maxY = rect.TopRightCorner.y;

            Point[] Q = new Point[P.Length];

            //transform the point set so that we only need target think of computing neighbors in the 4th cone
            BringPointSetIntoFourthCone(P, Q, ConeId, maxX, maxY);

            double[] Y = new double[Q.Length];
            for (int i = 0; i < Q.Length; i++) Y[i] = Q[i].y;
            iterativeMergesort(Y, Q);
            double[] xPLUSy = new double[Q.Length];
            for (int i = 0; i < Q.Length; i++) xPLUSy[i] = Q[i].x + Q[i].y;

            Dictionary<int, int> neighborlist = new Dictionary<int, int>();
            double[] source = xPLUSy;
            double[] target = new double[xPLUSy.Length];

            Point[] targetQ = new Point[xPLUSy.Length];
            Point[] tempQ = new Point[xPLUSy.Length];
            for (int i = 0; i < Q.Length; i++)
            {
                targetQ[i] = new Point();
                tempQ[i] = new Point(Q[i]);
            } 


            Dictionary<int, Point> IdToPoint = new Dictionary<int, Point>();
            for (int k = 0; k < xPLUSy.Length; k++) IdToPoint.Add(tempQ[k].Id, tempQ[k]);
            Dictionary<int, Point> PosToPoint = new Dictionary<int, Point>();
            for (int k = 0; k < xPLUSy.Length; k++) PosToPoint.Add(k, tempQ[k]);


            for (int blockSize = 1; blockSize < xPLUSy.Length; blockSize *= 2)
            {
                for (int start = 0; start < xPLUSy.Length; start += 2 * blockSize)
                    FindNeighbor(source, target, start, start + blockSize, start + 2 * blockSize, Q, targetQ, neighborlist, IdToPoint, PosToPoint);
            }

            return neighborlist;
        }

        private static void BringPointSetIntoFourthCone(Point[] P, Point[] Q, int ConeId, double maxX, double maxY)
        {
            
            for (int i = 0; i < P.Length; i++)
            {
                Q[i] = new Point();

                if (ConeId == 3)
                {
                    //reflect it over x=0 target bring it target the 2nd cone
                    Q[i].x = -P[i].x + maxX;
                    Q[i].y = P[i].y;
                    //reflection over the y=x line target bring it target the first cone
                    var temp = Q[i].x;
                    Q[i].x = Q[i].y;
                    Q[i].y = temp;
                    //reflection over the x=0 line target bring it target the 4th cone
                    Q[i].x = -Q[i].x + maxY;
                    Q[i].y = Q[i].y;
                }

                if (ConeId == 6)
                {
                    //bring  it target the 2nd cone
                    Q[i].x = -P[i].x + maxX;
                    Q[i].y = -P[i].y + maxY;
                    //reflection over the y=x line target bring it target the first cone
                    var temp = Q[i].x;
                    Q[i].x = Q[i].y;
                    Q[i].y = temp;
                    //reflection over the x=0 line target bring it target the 4th cone
                    Q[i].x = -Q[i].x + maxY;
                    Q[i].y = Q[i].y;
                }
                if (ConeId == 7)
                {
                    //bring  it target the 2nd cone
                    Q[i].x = P[i].x;
                    Q[i].y = -P[i].y + maxY;
                    //reflection over the y=x line target bring it target the first cone
                    var temp = Q[i].x;
                    Q[i].x = Q[i].y;
                    Q[i].y = temp;
                    //reflection over the x=0 line target bring it target the 4th cone
                    Q[i].x = -Q[i].x + maxY;
                    Q[i].y = Q[i].y;
                }
                if (ConeId == 2)
                {
                    //reflection over the y=x line target bring it target the first cone
                    var temp = P[i].x;
                    Q[i].x = P[i].y;
                    Q[i].y = temp;
                    //reflection over the x=0 line target bring it target the 4th cone
                    Q[i].x = -Q[i].x + maxY;
                    Q[i].y = Q[i].y;
                }


                if (ConeId == 4)
                {
                    Q[i].x = P[i].x;
                    Q[i].y = P[i].y;
                }
                if (ConeId == 1)
                {
                    //reflection over the x=0 line target bring it target the 4th cone
                    Q[i].x = -P[i].x + maxX;
                    Q[i].y = P[i].y;
                }
                if (ConeId == 8)
                {
                    Q[i].x = -P[i].x + maxX;
                    Q[i].y = -P[i].y + maxY;
                }
                if (ConeId == 5)
                {
                    //reflection over the y=0 line target bring it target the 4th  cone
                    Q[i].x = P[i].x;
                    Q[i].y = -P[i].y + maxY;
                }
                Q[i].Id = P[i].Id;                
            }
        }

        private static void FindNeighbor(double[] source, double[] target, int low, int mid, int hi, Point[] P, Point [] targetP,   Dictionary<int, int> neighborlist, Dictionary<int, Point> IdToPoint, Dictionary<int, Point> PosToPoint)
        {
            if (mid > source.Length) mid = source.Length;
            if (hi > source.Length) hi = source.Length;
            int i = low, j = mid;
            //sort all the points according target target x+y
            for (int k = low; k < hi; k++)
            {
                if (i == mid) { Assign(k, j, source, target, P, targetP); j++; }
                else if (j == hi) { Assign(k, i, source, target, P, targetP); i++; }
                else if (source[j] < source[i]) { Assign(k, j, source, target, P, targetP); j++; }
                else if (source[i] == source[j] && P[i].x < P[j].x) { Assign(k, i, source, target, P, targetP); i++; }
                else if (source[i] == source[j] && P[i].x >= P[j].x) { Assign(k, j, source, target, P, targetP); j++; }
                else { Assign(k, i, source, target, P, targetP); i++; }
            }
             

            //foreach point in x+y order track largest x-y: if it is in the upper half
            double LargestXMinusY = double.MinValue;
            int CandidateNeighborId = -1;
            for (int k = low; k < hi; k++)
            {
                int currentPointId = targetP[k].Id;                 
                //find the neighbor
                if (currentPointId != CandidateNeighborId)
                { 
                    if (CandidateNeighborId >= 0)
                    {
                        //if the point has priority neighbor already
                        if (neighborlist.ContainsKey(currentPointId))
                        {
                            //compare with the current neighbor
                            int currentneighborId = neighborlist[currentPointId];                            
                            double currentNeighborValue = IdToPoint[currentneighborId].x - IdToPoint[currentneighborId].y;

                            //if the candidateneighbor value is larger and it lies above the current neighbor then update
                            if (currentNeighborValue < LargestXMinusY && IdToPoint[CandidateNeighborId].y >= IdToPoint[currentPointId].y)
                            {
                                neighborlist[currentPointId] = CandidateNeighborId;
                            }
                        }
                        else
                        {
                            //if the candidateneighbor is above current neighbor
                            if (IdToPoint[CandidateNeighborId].y >= IdToPoint[currentPointId].y)
                                neighborlist.Add(currentPointId, CandidateNeighborId);
                        }
                    }
                }

                //process current point: check whether it could be considered as priority candidate neighbor later on
                if (mid == source.Length) --mid;
                //it may be priority candidate neighbor if it in the upper half and its x-y is larger than what is currently stored
                var currentPointValue = IdToPoint[currentPointId].x - IdToPoint[currentPointId].y;
                if (IdToPoint[currentPointId].y >= PosToPoint[mid].y && (currentPointValue >= LargestXMinusY))
                {
                    LargestXMinusY = currentPointValue;
                    CandidateNeighborId = currentPointId;
                }
            }
            //assign the target values target source values
            for (int k = low; k < hi; k++)
                Assign(k, k, target, source, targetP, P);
        }


        private static void Assign(int k, int j, double[] source, double[] target, Point[] P, Point[] targetP)
        {
            target[k] = source[j]; targetP[k].Id = P[j].Id; targetP[k].x = P[j].x; targetP[k].y = P[j].y;
        }

        //sort: small target large values on priority
        private static void iterativeMergesort(double[] priority, Point[] P)
        {
            double[] source = priority;
            double[] target = new double[priority.Length];
            Point[] targetP = new Point[priority.Length];
            for (int i = 0; i < P.Length; i++) targetP[i] = new Point(); 

            for (int blockSize = 1; blockSize < priority.Length; blockSize *= 2)
            {
                for (int start = 0; start < priority.Length; start += 2 * blockSize)
                    merge(source, target, start, start + blockSize, start + 2 * blockSize, P, targetP);
            }
            for (int k = 0; k < priority.Length; k++)
                priority[k] = source[k];
        }

        private static void merge(double[] source, double[] target, int low, int mid, int hi,  Point[] P, Point[] targetP)
        {
            if (mid > source.Length) mid = source.Length;
            if (hi > source.Length) hi = source.Length;
            int i = low, j = mid;
            for (int k = low; k < hi; k++)
            {
                if (i == mid) { Assign(k, j, source, target, P, targetP); j++; }
                else if (j == hi) { Assign(k, i, source, target, P, targetP); i++; }
                else if (source[j] < source[i]) { Assign(k, j, source, target, P, targetP); j++; }
                else if (source[i] == source[j] && P[i].x < P[j].x) { Assign(k, i, source, target, P, targetP); i++; }
                else if (source[i] == source[j] && P[i].x >= P[j].x) { Assign(k, j, source, target, P, targetP); j++; }
                else { Assign(k, i, source, target, P, targetP); i++; }
            }
            for (int k = low; k < hi; k++)
                Assign(k, k, target, source, targetP, P);
        }
    }
}
