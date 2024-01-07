using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这里面封装了一些可能会用到的一些小工具函数
public class Utils
{
    //洗牌
    static public void Shuffle<T>(List<T> array)
    {
        int n = array.Count;

        for (int i = 0; i < n; i++)
        {

            int r = i + Random.Range(0, n - i);

            T t = array[r];

            array[r] = array[i];

            array[i] = t;

        }
    }

    //带权重随机
    static public int GetRandomWeightedIndex(List<float> weights)

    {

        // Get the total sum of all the weights.
        float weightSum = 0;
        for (int i = 0; i < weights.Count; ++i)
            weightSum += weights[i];

        // Step through all the possibilities, one by one, checking to see if each one is selected.

        int index = 0;

        int lastIndex = weights.Count - 1;

        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
                return index;

            // Remove the currunt item from the sum of total untested weights and try again.

            weightSum -= weights[index++];

        }

        // No other item was selected, so return very last index.
        return index;
    }

    //生成随机二维单位向量
    static public Vector3 RandomUnitVector3()
    {
        Vector3 v3 = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f),
            Random.Range(-10f, 10f));
        return v3.normalized;
    }

    //生成随机三维单位向量
    static public Vector2 RandomUnitVector2()
    {
        Vector2 v2 = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        return v2.normalized;
    }
}
