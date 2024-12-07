# Generated from LW2.g4 by ANTLR 4.13.2
from antlr4 import *
from io import StringIO
import sys
if sys.version_info[1] > 5:
    from typing import TextIO
else:
    from typing.io import TextIO


def serializedATN():
    return [
        4,0,7,57,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,
        6,7,6,1,0,1,0,1,0,1,1,1,1,1,2,1,2,1,3,1,3,4,3,25,8,3,11,3,12,3,26,
        1,4,4,4,30,8,4,11,4,12,4,31,1,4,1,4,4,4,36,8,4,11,4,12,4,37,3,4,
        40,8,4,1,5,1,5,5,5,44,8,5,10,5,12,5,47,9,5,1,5,1,5,1,6,4,6,52,8,
        6,11,6,12,6,53,1,6,1,6,0,0,7,1,1,3,2,5,3,7,4,9,5,11,6,13,7,1,0,5,
        2,0,65,90,97,122,3,0,48,57,65,90,97,122,1,0,48,57,2,0,10,10,13,13,
        3,0,9,10,13,13,32,32,62,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,
        1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,1,15,1,0,0,0,3,18,
        1,0,0,0,5,20,1,0,0,0,7,22,1,0,0,0,9,29,1,0,0,0,11,41,1,0,0,0,13,
        51,1,0,0,0,15,16,5,10,0,0,16,17,5,10,0,0,17,2,1,0,0,0,18,19,5,10,
        0,0,19,4,1,0,0,0,20,21,5,32,0,0,21,6,1,0,0,0,22,24,7,0,0,0,23,25,
        7,1,0,0,24,23,1,0,0,0,25,26,1,0,0,0,26,24,1,0,0,0,26,27,1,0,0,0,
        27,8,1,0,0,0,28,30,7,2,0,0,29,28,1,0,0,0,30,31,1,0,0,0,31,29,1,0,
        0,0,31,32,1,0,0,0,32,39,1,0,0,0,33,35,5,46,0,0,34,36,7,2,0,0,35,
        34,1,0,0,0,36,37,1,0,0,0,37,35,1,0,0,0,37,38,1,0,0,0,38,40,1,0,0,
        0,39,33,1,0,0,0,39,40,1,0,0,0,40,10,1,0,0,0,41,45,5,35,0,0,42,44,
        8,3,0,0,43,42,1,0,0,0,44,47,1,0,0,0,45,43,1,0,0,0,45,46,1,0,0,0,
        46,48,1,0,0,0,47,45,1,0,0,0,48,49,6,5,0,0,49,12,1,0,0,0,50,52,7,
        4,0,0,51,50,1,0,0,0,52,53,1,0,0,0,53,51,1,0,0,0,53,54,1,0,0,0,54,
        55,1,0,0,0,55,56,6,6,0,0,56,14,1,0,0,0,7,0,26,31,37,39,45,53,1,6,
        0,0
    ]

class LW2Lexer(Lexer):

    atn = ATNDeserializer().deserialize(serializedATN())

    decisionsToDFA = [ DFA(ds, i) for i, ds in enumerate(atn.decisionToState) ]

    T__0 = 1
    T__1 = 2
    T__2 = 3
    NAME = 4
    FLOAT = 5
    LINE_COMMENT = 6
    WS = 7

    channelNames = [ u"DEFAULT_TOKEN_CHANNEL", u"HIDDEN" ]

    modeNames = [ "DEFAULT_MODE" ]

    literalNames = [ "<INVALID>",
            "'\\n\\n'", "'\\n'", "' '" ]

    symbolicNames = [ "<INVALID>",
            "NAME", "FLOAT", "LINE_COMMENT", "WS" ]

    ruleNames = [ "T__0", "T__1", "T__2", "NAME", "FLOAT", "LINE_COMMENT", 
                  "WS" ]

    grammarFileName = "LW2.g4"

    def __init__(self, input=None, output:TextIO = sys.stdout):
        super().__init__(input, output)
        self.checkVersion("4.13.2")
        self._interp = LexerATNSimulator(self, self.atn, self.decisionsToDFA, PredictionContextCache())
        self._actions = None
        self._predicates = None


