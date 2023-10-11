a = input("Enter text: ").split(" ")
dct = {
    "01": "Yanvar",
    "02": "Fevral",
    "03": "Mart",
    "04": "Aprel",
    "05": "May",
    "06": "Iyun",
    "07": "Iyul",
    "08": "Avgust",
    "09": "Sentabr",
    "10": "Oktabr",
    "11": "Noyabr",
    "12": "Dekabr"
}
a[0] = a[0].split(".")
a[1] = a[1].split(":")
print(a[0][0], dct[a[0][1]], a[0][2], "yil", a[1][0], "soat", a[1][1], "minut")
