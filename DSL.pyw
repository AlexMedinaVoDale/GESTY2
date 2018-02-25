import urllib
import urllib2
import requests
import pafy
import time
from bs4 import BeautifulSoup
from raven import Client
import os
import argparse
import xml.etree.cElementTree as ET

parser = argparse.ArgumentParser()
parser.add_argument("-s", "--ser", help="increase output verbosity")
args = parser.parse_args()
search = args.ser
scrape_url="http://www.youtube.com/results?search_query="
filtro="&sp=EgQQARgB"
search_hardcode = search.replace(" ", "+")
search_hardcode = search.replace("[", "")
search_hardcode = search.replace("]", "")
sb_url = scrape_url + search_hardcode + filtro
sb_get = requests.get(sb_url)
soupeddata = BeautifulSoup(sb_get.content, "html.parser")
yt_links = soupeddata.find_all("a", {"class": "yt-uix-tile-link yt-ui-ellipsis yt-ui-ellipsis-2 yt-uix-sessionlink spf-link "})
root = ET.Element("root")
results = ET.SubElement(root, "results")
for x in range(0, 15):
    yt_href = yt_links[x].get("href")
    yt_title = yt_links[x].get("title")
    yt_url = 'https://www.youtube.com' + yt_href
    titles = ET.SubElement(results, "titles")
    titles.text = yt_title
    url = ET.SubElement(titles, "url")
    url.text = yt_url 
arbol = ET.ElementTree(root)
arbol.write("c:\GESTY\listsearch.xml")
