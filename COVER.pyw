from mutagen.mp3 import MP3
from mutagen.id3 import ID3, APIC, error
import json
import os.path
import urllib
import urllib2
import requests
import argparse
import wget
from pydub import AudioSegment

parser = argparse.ArgumentParser()
parser.add_argument("-m", "--mp3", help="increase output verbosity")
args = parser.parse_args()
os.chdir("c:\\GESTY\\")
search = args.mp3
search = search.replace("C:\\GESTY\\Musica\\", "")
search = search.replace(".webm", "")
search_hardcode = search.replace(" ", "+")
search_hardcode = search_hardcode.replace("]", "")
search_hardcode = search_hardcode.replace("[", "")
search_hardcode = search_hardcode.replace("|", "")
archivoDescargar = "https://itunes.apple.com/search?term=" + search_hardcode + "&limit=25"
archivoGuardar = "cover.json"
descarga = urllib2.urlopen(archivoDescargar)
ficheroGuardar=file(archivoGuardar,"w")
ficheroGuardar.write(descarga.read())
ficheroGuardar.close()
leer = json.loads(open('cover.json').read())
arturl = leer["results"][0]["artworkUrl100"].replace("100x100", "500x500")
filename = wget.download(arturl)
os.remove(archivoGuardar)
os.system('cls' if os.name=='nt' else 'clear')
inp = "c:\\GESTY\\Musica\\" + search + ".webm"
out = "c:\\GESTY\\Musica\\" + search + ".mp3"
AudioSegment.from_file(inp).export(out, format="mp3")
os.remove(inp)
print out
print "[INSERTANDO COVER...]"
audio = MP3(out, ID3=ID3)
try:
    audio.add_tags()
except error:
    pass
audio.tags.add(
    APIC(
        encoding=3, # 3 is for utf-8
        mime='image/jpeg', # image/jpeg or image/png
        type=3, # 3 is for the cover image
        desc=u'Cover',
        data=open('500x500bb.jpg', 'rb').read()
    )
)
audio.save(v2_version=3)
os.system('cls' if os.name=='nt' else 'clear')
os.remove('500x500bb.jpg')
