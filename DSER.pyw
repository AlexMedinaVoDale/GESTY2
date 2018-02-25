import pafy
import os
import requests
from bs4 import BeautifulSoup
from raven import Client
import argparse
import xml.etree.cElementTree as ET

parser = argparse.ArgumentParser()
parser.add_argument("-l", "--link", help="increase output verbosity")
args = parser.parse_args()
sb_url = args.link
sb_get = requests.get(sb_url)
soupeddata = BeautifulSoup(sb_get.content, "html.parser")
yt_links = soupeddata.find_all("a", {"class": "yt-uix-tile-link yt-ui-ellipsis yt-ui-ellipsis-2 yt-uix-sessionlink spf-link "})
yt_href = yt_links[0].get("href")
yt_title = yt_links[0].get("title")
yt_url = 'https://www.youtube.com' + yt_href
url = yt_url
video = pafy.new(url)
bestaudio = video.getbestaudio()
daudio = bestaudio.url
root = ET.Element("root")
results = ET.SubElement(root, "results")
play = ET.SubElement(results, "play")
play.text = daudio
arbol = ET.ElementTree(root)
arbol.write("c:\GESTY\listdown.xml")
