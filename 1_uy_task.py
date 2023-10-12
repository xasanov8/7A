class Airoport:
    def __init__(self, nom, ochilish_yili) -> None:
        self.nom = nom
        self.ochilish_yil = ochilish_yili

    def get_info(self) -> str:
        print(f"""
Airoport nomi: {self.nom}
Airoport ochilgan yili: {self.ochilish_yil}
""")

a1 = Airoport("Air №1", 1997)
a1.get_info()
