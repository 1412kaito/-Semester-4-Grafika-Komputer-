import pygame
from pygame.locals import *

from OpenGL.GL import *
from OpenGL.GLU import *

def cek(event):
	for e in event:
		if e.type== pygame.QUIT:
			return False;
	
	return True;

def drawFront(x, y, constant_z, size):
	glBegin(GL_LINE_STRIP)
	glVertex3f(x,y,constant_z)
	glVertex3f(x+size,y,constant_z)
	glVertex3f(x+size,y+size,constant_z)
	glVertex3f(x,y+size,constant_z)
	glVertex3f(x,y,constant_z)
	glEnd();

def drawSide(y, z, constant_x, size):
	glBegin(GL_LINE_STRIP)
	glVertex3f(constant_x,y,z)
	glVertex3f(constant_x,y+size,z)
	glVertex3f(constant_x,y+size,z+size)
	glVertex3f(constant_x,y,z+size)
	glVertex3f(constant_x,y,z)
	glEnd();
	
def drawTop(x, z, constant_y, size):
	glBegin(GL_LINE_STRIP)
	glVertex3f(x,constant_y,z)
	glVertex3f(x,constant_y,z+size)
	glVertex3f(x+size,constant_y,z+size)
	glVertex3f(x+size,constant_y,z)
	glVertex3f(x,constant_y,z)
	glEnd();

def drawCube(startX, startY, startZ, size):
	drawFront(startX, startY, startZ, size)
	drawFront(startX, startY, startZ+size, size)
	drawSide(startY, startZ, startX, size)
	drawSide(startY, startZ, startX+size, size)
	drawTop(startX, startZ, startY, size)
	drawTop(startX, startZ, startY+size, size)

def main():
	# logo = pygame.image.load("path")
	# pygame.display.set_icon(logo)
	#glRotatef(22.5, 0.75,0,0.75)
	# startX = int(input("X: "))
	# startY = int(input("Y: "))
	# startZ = int(input("Z: "))
	# size = int (input("Ukuran : "))
	startX=-2;
	startY=-2;
	startZ=-2;
	size=4;
	pygame.init()
	pygame.display.set_caption("nama program")
	running = True
	screen = pygame.display.set_mode((800,600), DOUBLEBUF|OPENGL)
	gluPerspective(45, (800/600), 0.1, 50.0)
	glTranslatef(0 ,0 , -20)
	glRotatef(45, 0,0.75,0)
	
	while running:
		glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
		drawCube(startX, startY, startZ, size)
		pygame.display.flip()
		glRotatef(1, 3, 1, 1)
		pygame.time.wait(10)
		running = cek(pygame.event.get())
		
	print ('Hello World');
	
main()
