import pafy
import argparse
import xml.etree.cElementTree as ET

parser = argparse.ArgumentParser()
parser.add_argument("-l", "--link", help="increase output verbosity")
args = parser.parse_args()
url = args.link
video = pafy.new(url)
bestaudio = video.getbestaudio()
daudio = bestaudio.url
root = ET.Element("root")
results = ET.SubElement(root, "results")
play = ET.SubElement(results, "play")
play.text = daudio
arbol = ET.ElementTree(root)
arbol.write("c:\GESTY\listdown.xml")
