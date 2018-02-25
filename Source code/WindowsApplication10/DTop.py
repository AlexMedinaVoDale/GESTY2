import urllib2
import webbrowser
import os
import urllib
import requests
import argparse

parser = argparse.ArgumentParser()
parser.add_argument("-i", "--ids", help="increase output verbosity")
args = parser.parse_args()
genre = args.ids
archivoDescargar = "https://itunes.apple.com/us/rss/topsongs/limit=100/genre=" + genre + "/explicit=true/xml"
archivoGuardar = "list.xml"
descarga = urllib2.urlopen(archivoDescargar)
ficheroGuardar=file(archivoGuardar,"w")
ficheroGuardar.write(descarga.read())
ficheroGuardar.close()
