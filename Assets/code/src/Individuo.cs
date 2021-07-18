﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.code.src
{
    public class Individuo : IComparable
    {
        public float []genes;
        public float fitness;
        static Random random = new Random();

        private int probMut = 15;

        // 3 neuronas conectadas a 2 salidas

        public Individuo(int tamanoGenes)
        {
            // generar un individuo con genes random
            genes = new float[tamanoGenes];

            for(int i = 0; i < genes.Length; i++)
            {
                genes[i] = GenerateRandomGene();
            }
        }
        
        private float GenerateRandomGene()
        {
            float gen = (float)random.NextDouble();

            // randomizar a negativo
            //if (random.Next(100) % 2 == 0)
            //{
            //    gen = -gen;
            //}
            return gen;
        }

        public String getGenes()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(float g in genes)
            {
                sb.Append(g);
                sb.Append(", ");
            }

            sb.Append(" Fitness: " + fitness);
            return sb.ToString();
        }

        public Individuo Cruzar(Individuo otro)
        {
            Individuo nuevo = new Individuo(genes.Length);

            for (int i = 0; i<genes.Length; i++)
            {
                if(random.Next(100) <= probMut)
                {
                    // mutate
                    nuevo.genes[i] = GenerateRandomGene();
                    continue;
                }
                
                nuevo.genes[i] = (genes[i] + otro.genes[i])/ 2;
            }

            return nuevo;
        }

        public int CompareTo(Object obj)
        {
            Individuo otro = (Individuo)obj;
            if (fitness < otro.fitness)
                return 1;

            if (fitness == otro.fitness)
                return 0;

            return -1;
        }
    }
}
