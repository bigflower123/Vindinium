using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*Recherche le plus court chemin entre le départ et la destination
 en utilisant l'algorithme A star */
namespace vindinium
{
    public class Node
    {
        public Node ParentNode { get; set; }
        public int F { get; private set; }
        public int G { get; private set; }
        public int H { get; private set; }
        public Pos currentPos { get; set; }
        public Node(int x, int y)
        {
            currentPos.x = x;
            currentPos.y = y;
        }
        public void CalculF()
        {
            this.F = this.G + this.H;
        }
    }

    class PlusCourtChemin
    {
        private List<Node> OpenList;
        private List<Node> ClosedList;
        public Tile[][] board { get; private set; }

        public void TrouveChemin(Node depart, Node destination)
        {
            Node tmpNode;
            List<Node> tmpListNode;
            OpenList.Add(depart);
            do{
                tmpNode = MinF(OpenList, depart, destination);
                OpenList.Remove(tmpNode);
                ClosedList.Add(tmpNode);
                tmpListNode = SurroundNode(tmpNode, board);
                foreach(Node UnNode in tmpListNode)
                {
                    if (!OpenList.Contains(UnNode))
                    {
                        OpenList.Add(UnNode);
                        UnNode.ParentNode = tmpNode;
                    }
                    else
                    {

                    }
                }

            }while(OpenList.Contains(destination));

        }

        /// <summary>
        /// Recherche les nodes autour d'un point qui est accessible
        /// </summary>
        /// <param name="currentNode">Le point courant</param>
        /// <returns></returns>
        public List<Node> SurroundNode(Node currentNode, Tile[][] board)
        {
            List<Node> SurrondNode = new List<Node>(4);
            int currentX = currentNode.currentPos.x;
            int currentY = currentNode.currentPos.y;
            for (int x = currentX - 1; x < currentX + 1; x = x + 2)
            {
                for (int y = currentY - 1; y < currentY + 1; y = y + 2)
                {
                    Node tmpNode = new Node(x, y);
                    if (board[x][y] == Tile.FREE && !ClosedList.Contains(tmpNode))
                    { 
                        SurrondNode.Add(tmpNode);
                    }
                }
            }
            return SurrondNode;  
        }

      

        /// <summary>
        /// Calculer le coût de mouvement
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="depart"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public int CalculCost(Node currentNode, Node depart, Node destination)
        {
            int G = currentNode.ParentNode.G + 10;
            int ecartX = destination.currentPos.x - currentNode.currentPos.x;
            int ecartY = destination.currentPos.y - currentNode.currentPos.y;
            int H = (int)Math.Sqrt(ecartX * ecartX + ecartY * ecartY);
            int F = G + H;
            return F;
        }

        /// <summary>
        /// Retourner le point dans la Openlist qui est avec les valeurs minimum de F
        /// </summary>
        /// <param name="list"></param>
        /// <param name="depart"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Node MinF(List<Node> list, Node depart, Node destination)
        {
            int ValF;
            int MinF = CalculCost(list[0], depart, destination);
            Node MinNode = list[0];
            foreach (Node tmpNode in list)
            {
                ValF = CalculCost(tmpNode, depart, destination);
                if (ValF < MinF)
                {
                    MinF = ValF;
                    MinNode = tmpNode;
                }

            }
            return MinNode;
        }
    }
}
