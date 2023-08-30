# ---------------------------------------------------------------------------
VERSION = BCB.01
# ---------------------------------------------------------------------------
!ifndef BCB
BCB = $(MAKEDIR)\..
!endif
# ---------------------------------------------------------------------------
PROJECT = Connect4.exe
OBJFILES = Connect4.obj Main.obj AlphaBetaTree\AlphaBetaTree.obj \
   AlphaBetaTree\State\State.obj about.obj functions.obj OptionsDialog.obj
RESFILES = Connect4.res
RESDEPEN = $(RESFILES) Main.dfm about.dfm OptionsDialog.dfm
LIBFILES = 
DEFFILE = 
# ---------------------------------------------------------------------------
CFLAG1 = -Od -Hc -w -k -r- -y -v -vi- -c -a4 -b- -w-par -w-inl -Vx -Ve -x
CFLAG2 = -D__WARN \
   -I.\background;.\alphabetatree\state;.\alphabetatree;$(BCB)\projects;$(BCB)\include;$(BCB)\include\vcl \
   -H=$(BCB)\lib\vcld.csm 
PFLAGS = -AWinTypes=Windows;WinProcs=Windows;DbiTypes=BDE;DbiProcs=BDE;DbiErrs=BDE \
   -D__WARN \
   -U.\background;.\alphabetatree\state;.\alphabetatree;$(BCB)\projects;$(BCB)\lib\obj;$(BCB)\lib \
   -I.\background;.\alphabetatree\state;.\alphabetatree;$(BCB)\projects;$(BCB)\include;$(BCB)\include\vcl \
   -v -$Y -$W -$O- -JPHNV -M  
RFLAGS = -D__WARN \
   -i.\background;.\alphabetatree\state;.\alphabetatree;$(BCB)\projects;$(BCB)\include;$(BCB)\include\vcl 
LFLAGS = -L.\background;.\alphabetatree\state;.\alphabetatree;$(BCB)\projects;$(BCB)\lib\obj;$(BCB)\lib \
   -aa -Tpe -x -v -V4.0 
IFLAGS = 
LINKER = ilink32
# ---------------------------------------------------------------------------
ALLOBJ = c0w32.obj $(OBJFILES)
ALLRES = $(RESFILES)
ALLLIB = $(LIBFILES) vcl.lib import32.lib cp32mt.lib 
# ---------------------------------------------------------------------------
.autodepend

$(PROJECT): $(OBJFILES) $(RESDEPEN) $(DEFFILE)
    $(BCB)\BIN\$(LINKER) @&&!
    $(LFLAGS) +
    $(ALLOBJ), +
    $(PROJECT),, +
    $(ALLLIB), +
    $(DEFFILE), +
    $(ALLRES) 
!

.pas.hpp:
    $(BCB)\BIN\dcc32 $(PFLAGS) { $** }

.pas.obj:
    $(BCB)\BIN\dcc32 $(PFLAGS) { $** }

.cpp.obj:
    $(BCB)\BIN\bcc32 $(CFLAG1) $(CFLAG2) -o$* $* 

.c.obj:
    $(BCB)\BIN\bcc32 $(CFLAG1) $(CFLAG2) -o$* $**

.rc.res:
    $(BCB)\BIN\brcc32 $(RFLAGS) $<
#-----------------------------------------------------------------------------
