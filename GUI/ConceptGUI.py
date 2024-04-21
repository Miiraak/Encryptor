from tkinter import *
MainWindows=Tk()

# Zone de User Prompt
UserPrompt=Entry(MainWindows,width=33, bd=3)
UserPrompt.place(x=11, y=321)

# Boutton d'illustration
btn=Button(MainWindows, text='Illustration', width=10, fg='blue')
btn.place(x=225, y=318)

# Zone de Master Prompt
MasterCanvas=Canvas(MainWindows, bg="Black", height=300, width=293)
MasterCanvasLabel=Label(MasterCanvas, bg='white', text='Master Prompt')
MasterCanvasLabel.place(x=0, y=0)
MasterCanvas.pack()
MasterCanvas.place(x=10, y=10)

# Zone de l'inventaire
InventoryCanvas=Canvas(MainWindows, bg="Black", height=170, width=290)
InventoryCanvasLabel=Label(InventoryCanvas, bg='white', text='Inventory')
InventoryCanvasLabel.place(x=0, y=0)
InventoryCanvas.pack()
InventoryCanvas.place(x=325, y=10)

# Zone des attributs
AttributsCanvas=Canvas(MainWindows, bg='Black', height=150, width=290)
AttributsCanvasLabel=Label(AttributsCanvas, bg='White', text='Attributs')
AttributsCanvasLabel.place(x=0, y=0)
AttributsCanvas.pack()
AttributsCanvas.place(x=325, y=190)

MainWindows.title('AIAR - AI Assisted RPG')
MainWindows.geometry("625x355+10+20")
MainWindows.mainloop()
