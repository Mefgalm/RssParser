﻿namespace RssParser

type Item =
    { Title: string
      Link: string
      Description: string
      Categories: string array }
    
namespace RssParser.FSharp 
    open RssParser
    
    module RssExtractor =
        open System.Xml
        open FSharp.Data

        let private selectInnerText key (xmlNode: XmlNode) =
            let node = xmlNode.SelectSingleNode key
            if isNull node then
                ""
            else
                node.InnerText

        let private parseItem xmlNode =
            { Title = xmlNode |> selectInnerText "title"
              Link = xmlNode |> selectInnerText "link"
              Description = xmlNode |> selectInnerText "description"
              Categories = xmlNode.SelectNodes "category"
                           |> Seq.cast<XmlNode>
                           |> Seq.map(fun node -> node.InnerText)
                           |> Seq.toArray }

        let getItems url = async {
            let! responseString = Http.AsyncRequestString url
            
            let doc = XmlDocument()
            doc.LoadXml responseString
            
            return
                doc.SelectNodes "/rss/channel/item"
                |> Seq.cast<XmlNode>
                |> Seq.map parseItem
        }

namespace RssParser
    
open System.Threading.Tasks
open System.Collections.Generic
open RssParser.FSharp

type RssExtractor =
    static member GetItemsAsync(url: string): Task<IEnumerable<Item>> =
        RssExtractor.getItems url |> Async.StartAsTask