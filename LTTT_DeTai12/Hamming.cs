using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LTTT_DeTai12
{
    public class Hamming
    {
        public List<List<bool>> split(List<bool> lst, int size)
        {
            //bool[][] a = new bool[size][];
            List<List<bool>> list = new List<List<bool>>();
            int x = (int)Math.Ceiling((float)lst.Count / size);
            int y = 0;
            for (int i = 0; i < x; i++)
            {
                List<bool> a = new List<bool>();
                for (int j = 0; j < size; j++)
                {
                    if (y < lst.Count)
                        a.Add(lst[y]);
                    else
                        a.Add(false);
                    y++;
                }
                list.Add(a);
            }
            return list;

        }
        public List<bool> encode(List<bool> m, List<List<bool>> matrix)
        {
            List<bool> lst = new List<bool>();
            for(int i=0;i<matrix[0].Count;i++)
            {
                bool a= false;
                for(int j=0;j<matrix.Count;j++)
                {
                    a = (a ^ (matrix[j][i] & m[j]));
                }
                lst.Add(a);
            }
            return lst;
        }


        public List<bool> decode(List<bool> m,int size)
        {
            bool x1 = (m[0] ^ m[1] ^ m[3]);
            bool x2 = (m[0] ^ m[2] ^ m[3]);
            bool x3 = (m[1] ^ m[2] ^ m[3]);
            if (x1 != m[4])
            {
                if (x2 != m[5])
                {
                    if (x3 != m[6])
                    {
                        m[3] ^= true;
                    }
                    else
                    {
                        m[0] ^= true;
                    }
                }
                else
                {
                    if (x3 != m[6])
                        m[1] ^= true;
                    else
                        m[4] ^= true;
                }
            }
            else
            {
                if (x2 != m[5])
                {
                    if (x3 != m[6])
                        m[2] ^= true;
                    else
                        m[5] ^= true;
                }
                else
                {
                    if (x3 != m[6])
                        m[6] ^= true;
                }
            }
            m.RemoveRange(size, (m.Count - size));
            return m;

        }
        public List<bool> decode_matrix(List<bool> m, List<List<bool>> matrix)
        {
            List<List<bool>> matrix_nd = new List<List<bool>>();
            List<List<bool>> matrix_dao = new List<List<bool>>();
            for (int i = 0; i<m.Count; i++)
            {
                List<bool> item = new List<bool>();
                for (int j = 0; j < matrix.Count; j++)
                {
                    item.Add(matrix[j][i]);
                }
                if (m[i])
                    matrix_nd.Add(item);
                matrix_dao.Add(item);


            }
            if(matrix_nd.Count==0)
            {
                m.RemoveRange(m.Count - 3, 3);
                return m;
            }    
            List<bool> check = new List<bool>();
            for(int i =  0; i< matrix_nd[0].Count;i++)
            {
                bool item = false;
                for(int j = 0; j<matrix_nd.Count;j++)
                {
                    item ^= matrix_nd[j][i];
                }
                check.Add(item);
            }
            if(!check.Contains(true))
            {
                m.RemoveRange(m.Count - 3, 3);
                return m;
            }
            else
            {
                int vt = vtcheck(matrix_dao, check);
                m[vt] ^= true;
                m.RemoveRange(m.Count - 3, 3);
                return m;
            }    


        }
        public int vtcheck(List<List<bool>> matrix_dao , List<bool> check)
        {
            int kq = -1;
            for (int i = 0; i < matrix_dao.Count; i++)
            {
                int dem = 0;
                for (int j = 0; j < matrix_dao[i].Count; j++)
                {
                    if (matrix_dao[i][j] == check[j])
                        dem++;
                }
                if (dem == check.Count())
                    kq = i;
            }
            return kq;
        }
        public List<List<bool>> matrangoloi(List<List<bool>> matrix)
        {

            //List<bool> lst = new List<bool>();
            List<List<bool>> lst = new List<List<bool>>();
            List<List<string>> matran = new List<List<string>>();
            for (int i = 0; i < matrix.Count; i++)
            {
                List<string> item = new List<string>();
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    int a = matrix[i][j] ? 1 : 0;
                    if (a == 1)
                        item.Add("a" + j);
                }
                matran.Add(item);
            }
            return lst;
        }
        public List<bool> random_error(List<bool> lst, float p_err)
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < lst.Count; i++)
            {
                var rand = new Random();     
                double t = rand.NextDouble();
                Thread.Sleep(1);
                if (t < p_err)
                    list.Add(lst[i] ^ true);
                else
                    list.Add(lst[i]);
            }
            return list;
        }


    }
}
