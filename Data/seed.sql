/*  To seed data:
    sqlite3 auction.db < create.sql
*/

insert into Users (firstname, lastname, username, email)
    values
        ('Graham','Rogers','grogers', 'gmrogers04@gmail.com'),
        ('Kristen','Rogers','krogers','kristendollahite@gmail.com')
        ('Ross','Brandon','rbrandon','ross.brandon3@gmail.com')

insert into Vehicles (make,model,vin)
    values
        ('Jeep','Wrangler','1234asdf'),
        ('Ford','Escape','1a2s3d4f'),
        ('Subaru','BRZ','fdsa4321');