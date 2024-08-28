import csv
import json
import xml.etree.ElementTree as ET

categorias = {}
with open('categories.csv', 'r') as file:
    reader = csv.DictReader(file, delimiter=';')
    for row in reader:
        # Cada categoría se almacena en el diccionario `categorias` con su 'Id' como clave.
        categorias[row['Id']] = {
            'Id': int(row['Id']),  #Se convierte el Id a entero.
            'Name': row['Name'].strip(),  #Se quitan los espacios en blanco del nombre.
            'Description': row['Description'].strip(),  #Se quitan los espacios en blanco de la descripcion.
            'Products': []  #Se inicializa una lista vacía para los productos de cada categoría.
        }

#Lee el archivo Products.csv y asocia los productos a sus categorías.
with open('Products.csv', 'r') as file:
    reader = csv.DictReader(file, delimiter=';')
    for row in reader:
        #Cada fila de producto se convierte en un diccionario con los datos.
        product = {
            'Id': int(row['Id']),  #Se convierte el Id del producto a entero.
            'CategoryId': int(row['CategoryId']),  #Se convierte el Id de categoría del producto a entero.
            'Name': row['Name'].strip(),  #Se quitan los espacios en blanco del nombre del producto.
            'Price': float(row['Price'].replace(',', '.'))  #Se convierte el precio de cadena a float y tambien sustituye la coma por punto.
        }
        #Se añade el producto a la lista de productos de la categoría correspondiente en el diccionario `categorias`.
        categorias[row['CategoryId']]['Products'].append(product)

#Genera el archivo Catalog.json con los datos de categorías y productos.
with open('Catalog.json', 'w', encoding='utf-8') as json_file:
    #Se usa `indent=4` para que el archivo tenga un formato legible, con una indentación de 4 espacios.
    json.dump(list(categorias.values()), json_file, indent=4, ensure_ascii=False)

root = ET.Element("ArrayOfCategory", xmlns="http://schemas.datacontract.org/2004/07/Catalog", attrib={"xmlns:i": "http://www.w3.org/2001/XMLSchema-instance"})

#Recorre todas las categorías en el diccionario `categorias`.
for category in categorias.values():
    #Genera el xml estrucutrado
    category_element = ET.SubElement(root, "Category")
    
    id_element = ET.SubElement(category_element, "Id")
    id_element.text = str(category['Id']) 
    
    name_element = ET.SubElement(category_element, "Name")
    name_element.text = category['Name']
    

    description_element = ET.SubElement(category_element, "Description")
    description_element.text = category['Description']
    
    products_element = ET.SubElement(category_element, "Products")
    
    for product in category['Products']:
        product_element = ET.SubElement(products_element, "Product")
        
        #Añade los elementos <Id>, <CategoryId>, <Name> y <Price> dentro de cada <Product>.
        prod_id_element = ET.SubElement(product_element, "Id")
        prod_id_element.text = str(product['Id'])
        
        prod_catid_element = ET.SubElement(product_element, "CategoryId")
        prod_catid_element.text = str(product['CategoryId'])
        
        prod_name_element = ET.SubElement(product_element, "Name")
        prod_name_element.text = product['Name']
        
        prod_price_element = ET.SubElement(product_element, "Price")
        prod_price_element.text = str(product['Price'])

#Crea un árbol XML a partir del elemento raíz y lo guarda en un archivo Catalog.xml.
tree = ET.ElementTree(root)
tree.write("Catalog.xml", encoding='utf-8', xml_declaration=True) 
