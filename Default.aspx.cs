using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    #region "Declaring Global variables   
    string firstLongestWord;
    string secondLongestWord;
    string secondLongestWordnoconcatenation;
    #endregion

    #region "Page Load event"
    // The main function is called to fetch the first and the second longest word from the word list
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilePath = @"C:\Users\xe2zkuma\Desktop\wordlist.txt";
       string[] wordlist = File.ReadLines(FilePath).ToArray();
       firstLongestWord = getFirstLongWord(wordlist);
       secondLongestWord = getSecondLongWord(wordlist);            
       Response.Write("The first longest word [concatenated] from the word list is: " + firstLongestWord + "<br/> " + "The second longest word [concatenated] from the word list is: " + secondLongestWord + "<br/>" + "The second longest word from the word list: " + secondLongestWordnoconcatenation );                          
    }

    #endregion

    #region "Function to fetch the First Longest word"
    public string getFirstLongWord(string[] wordlist)
    {
        if (wordlist == null) throw new ArgumentException("listOfWords");
        var sortedWords = wordlist.OrderByDescending(word => word.Length).ToList();
        secondLongestWordnoconcatenation = sortedWords[1].ToString();
        var dict = new HashSet<String>(sortedWords);
        foreach (var word in sortedWords)
        {
            if (isConcatenated(word, dict))
            {
                return word;                
            }
        }
        return null;
    }

    #endregion

    #region "Function to fetch the second longest word"
    public string getSecondLongWord(string[] wordlist)
    {
        if (wordlist == null) throw new ArgumentException("listOfWords");
        var sortedWords = wordlist.OrderByDescending(word => word.Length).ToList();
        secondLongestWord = sortedWords[1].ToString();
        var dict = new HashSet<String>(sortedWords);
        foreach (var word in sortedWords)
        {
            if (isConcatenated(word, dict))
            {
                if(word!=firstLongestWord)
                return word;
            }
        }
        return null;
    }

    #endregion

    #region "Function that identifies the concatenated word"
    private bool isConcatenated(string word, HashSet<string> dict)
    {
        if (String.IsNullOrEmpty(word)) return false;
        if (word.Length == 1)
        {
            if (dict.Contains(word)) return true;
            else return false;
        }
        foreach (var pair in getPairs(word))
        {
            if (dict.Contains(pair.Item1))
            {
                if (dict.Contains(pair.Item2))
                {                    
                    return true;
                }
                else
                {
                    return isConcatenated(pair.Item2, dict);
                }
            }
        }
        return false;
    }
    #endregion

    #region "Form the Pairs for the provided word"
    private static List<Tuple<string, string>> getPairs(string word)
    {
        var output = new List<Tuple<string, string>>();
        for (int i = 1; i < word.Length; i++)
        {
            output.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
        }
        return output;
    }
    #endregion
}