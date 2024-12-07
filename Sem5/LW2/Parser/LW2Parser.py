# Generated from LW2.g4 by ANTLR 4.13.2
# encoding: utf-8
from antlr4 import *
from io import StringIO
import sys
if sys.version_info[1] > 5:
	from typing import TextIO
else:
	from typing.io import TextIO

def serializedATN():
    return [
        4,1,7,70,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,
        2,7,7,7,2,8,7,8,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,2,1,2,1,2,1,2,
        1,3,1,3,1,3,1,3,1,3,4,3,36,8,3,11,3,12,3,37,1,4,1,4,5,4,42,8,4,10,
        4,12,4,45,9,4,1,4,4,4,48,8,4,11,4,12,4,49,1,5,1,5,5,5,54,8,5,10,
        5,12,5,57,9,5,1,5,4,5,60,8,5,11,5,12,5,61,1,6,1,6,1,7,1,7,1,8,1,
        8,1,8,0,0,9,0,2,4,6,8,10,12,14,16,0,0,65,0,18,1,0,0,0,2,23,1,0,0,
        0,4,26,1,0,0,0,6,30,1,0,0,0,8,39,1,0,0,0,10,51,1,0,0,0,12,63,1,0,
        0,0,14,65,1,0,0,0,16,67,1,0,0,0,18,19,3,2,1,0,19,20,3,4,2,0,20,21,
        5,1,0,0,21,22,3,6,3,0,22,1,1,0,0,0,23,24,5,4,0,0,24,25,5,1,0,0,25,
        3,1,0,0,0,26,27,3,8,4,0,27,28,5,2,0,0,28,29,3,10,5,0,29,5,1,0,0,
        0,30,31,3,8,4,0,31,32,5,2,0,0,32,35,3,10,5,0,33,34,5,2,0,0,34,36,
        3,10,5,0,35,33,1,0,0,0,36,37,1,0,0,0,37,35,1,0,0,0,37,38,1,0,0,0,
        38,7,1,0,0,0,39,47,3,12,6,0,40,42,3,16,8,0,41,40,1,0,0,0,42,45,1,
        0,0,0,43,41,1,0,0,0,43,44,1,0,0,0,44,46,1,0,0,0,45,43,1,0,0,0,46,
        48,3,12,6,0,47,43,1,0,0,0,48,49,1,0,0,0,49,47,1,0,0,0,49,50,1,0,
        0,0,50,9,1,0,0,0,51,59,3,14,7,0,52,54,3,16,8,0,53,52,1,0,0,0,54,
        57,1,0,0,0,55,53,1,0,0,0,55,56,1,0,0,0,56,58,1,0,0,0,57,55,1,0,0,
        0,58,60,3,14,7,0,59,55,1,0,0,0,60,61,1,0,0,0,61,59,1,0,0,0,61,62,
        1,0,0,0,62,11,1,0,0,0,63,64,5,4,0,0,64,13,1,0,0,0,65,66,5,5,0,0,
        66,15,1,0,0,0,67,68,5,3,0,0,68,17,1,0,0,0,5,37,43,49,55,61
    ]

class LW2Parser ( Parser ):

    grammarFileName = "LW2.g4"

    atn = ATNDeserializer().deserialize(serializedATN())

    decisionsToDFA = [ DFA(ds, i) for i, ds in enumerate(atn.decisionToState) ]

    sharedContextCache = PredictionContextCache()

    literalNames = [ "<INVALID>", "'\\n\\n'", "'\\n'", "' '" ]

    symbolicNames = [ "<INVALID>", "<INVALID>", "<INVALID>", "<INVALID>", 
                      "NAME", "FLOAT", "LINE_COMMENT", "WS" ]

    RULE_kb = 0
    RULE_header = 1
    RULE_implication = 2
    RULE_rule = 3
    RULE_elements = 4
    RULE_truth_degrees = 5
    RULE_element = 6
    RULE_membership_degree = 7
    RULE_separator = 8

    ruleNames =  [ "kb", "header", "implication", "rule", "elements", "truth_degrees", 
                   "element", "membership_degree", "separator" ]

    EOF = Token.EOF
    T__0=1
    T__1=2
    T__2=3
    NAME=4
    FLOAT=5
    LINE_COMMENT=6
    WS=7

    def __init__(self, input:TokenStream, output:TextIO = sys.stdout):
        super().__init__(input, output)
        self.checkVersion("4.13.2")
        self._interp = ParserATNSimulator(self, self.atn, self.decisionsToDFA, self.sharedContextCache)
        self._predicates = None




    class KbContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def header(self):
            return self.getTypedRuleContext(LW2Parser.HeaderContext,0)


        def implication(self):
            return self.getTypedRuleContext(LW2Parser.ImplicationContext,0)


        def rule_(self):
            return self.getTypedRuleContext(LW2Parser.RuleContext,0)


        def getRuleIndex(self):
            return LW2Parser.RULE_kb

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterKb" ):
                listener.enterKb(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitKb" ):
                listener.exitKb(self)




    def kb(self):

        localctx = LW2Parser.KbContext(self, self._ctx, self.state)
        self.enterRule(localctx, 0, self.RULE_kb)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 18
            self.header()
            self.state = 19
            self.implication()
            self.state = 20
            self.match(LW2Parser.T__0)
            self.state = 21
            self.rule_()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class HeaderContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def NAME(self):
            return self.getToken(LW2Parser.NAME, 0)

        def getRuleIndex(self):
            return LW2Parser.RULE_header

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterHeader" ):
                listener.enterHeader(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitHeader" ):
                listener.exitHeader(self)




    def header(self):

        localctx = LW2Parser.HeaderContext(self, self._ctx, self.state)
        self.enterRule(localctx, 2, self.RULE_header)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 23
            self.match(LW2Parser.NAME)
            self.state = 24
            self.match(LW2Parser.T__0)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ImplicationContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def elements(self):
            return self.getTypedRuleContext(LW2Parser.ElementsContext,0)


        def truth_degrees(self):
            return self.getTypedRuleContext(LW2Parser.Truth_degreesContext,0)


        def getRuleIndex(self):
            return LW2Parser.RULE_implication

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterImplication" ):
                listener.enterImplication(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitImplication" ):
                listener.exitImplication(self)




    def implication(self):

        localctx = LW2Parser.ImplicationContext(self, self._ctx, self.state)
        self.enterRule(localctx, 4, self.RULE_implication)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 26
            self.elements()
            self.state = 27
            self.match(LW2Parser.T__1)
            self.state = 28
            self.truth_degrees()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class RuleContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def elements(self):
            return self.getTypedRuleContext(LW2Parser.ElementsContext,0)


        def truth_degrees(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(LW2Parser.Truth_degreesContext)
            else:
                return self.getTypedRuleContext(LW2Parser.Truth_degreesContext,i)


        def getRuleIndex(self):
            return LW2Parser.RULE_rule

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterRule" ):
                listener.enterRule(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitRule" ):
                listener.exitRule(self)




    def rule_(self):

        localctx = LW2Parser.RuleContext(self, self._ctx, self.state)
        self.enterRule(localctx, 6, self.RULE_rule)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 30
            self.elements()
            self.state = 31
            self.match(LW2Parser.T__1)
            self.state = 32
            self.truth_degrees()
            self.state = 35 
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while True:
                self.state = 33
                self.match(LW2Parser.T__1)
                self.state = 34
                self.truth_degrees()
                self.state = 37 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if not (_la==2):
                    break

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ElementsContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def element(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(LW2Parser.ElementContext)
            else:
                return self.getTypedRuleContext(LW2Parser.ElementContext,i)


        def separator(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(LW2Parser.SeparatorContext)
            else:
                return self.getTypedRuleContext(LW2Parser.SeparatorContext,i)


        def getRuleIndex(self):
            return LW2Parser.RULE_elements

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterElements" ):
                listener.enterElements(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitElements" ):
                listener.exitElements(self)




    def elements(self):

        localctx = LW2Parser.ElementsContext(self, self._ctx, self.state)
        self.enterRule(localctx, 8, self.RULE_elements)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 39
            self.element()
            self.state = 47 
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while True:
                self.state = 43
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==3:
                    self.state = 40
                    self.separator()
                    self.state = 45
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 46
                self.element()
                self.state = 49 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if not (_la==3 or _la==4):
                    break

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class Truth_degreesContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def membership_degree(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(LW2Parser.Membership_degreeContext)
            else:
                return self.getTypedRuleContext(LW2Parser.Membership_degreeContext,i)


        def separator(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(LW2Parser.SeparatorContext)
            else:
                return self.getTypedRuleContext(LW2Parser.SeparatorContext,i)


        def getRuleIndex(self):
            return LW2Parser.RULE_truth_degrees

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterTruth_degrees" ):
                listener.enterTruth_degrees(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitTruth_degrees" ):
                listener.exitTruth_degrees(self)




    def truth_degrees(self):

        localctx = LW2Parser.Truth_degreesContext(self, self._ctx, self.state)
        self.enterRule(localctx, 10, self.RULE_truth_degrees)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 51
            self.membership_degree()
            self.state = 59 
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while True:
                self.state = 55
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==3:
                    self.state = 52
                    self.separator()
                    self.state = 57
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 58
                self.membership_degree()
                self.state = 61 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if not (_la==3 or _la==5):
                    break

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ElementContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def NAME(self):
            return self.getToken(LW2Parser.NAME, 0)

        def getRuleIndex(self):
            return LW2Parser.RULE_element

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterElement" ):
                listener.enterElement(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitElement" ):
                listener.exitElement(self)




    def element(self):

        localctx = LW2Parser.ElementContext(self, self._ctx, self.state)
        self.enterRule(localctx, 12, self.RULE_element)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 63
            self.match(LW2Parser.NAME)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class Membership_degreeContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def FLOAT(self):
            return self.getToken(LW2Parser.FLOAT, 0)

        def getRuleIndex(self):
            return LW2Parser.RULE_membership_degree

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterMembership_degree" ):
                listener.enterMembership_degree(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitMembership_degree" ):
                listener.exitMembership_degree(self)




    def membership_degree(self):

        localctx = LW2Parser.Membership_degreeContext(self, self._ctx, self.state)
        self.enterRule(localctx, 14, self.RULE_membership_degree)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 65
            self.match(LW2Parser.FLOAT)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class SeparatorContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser


        def getRuleIndex(self):
            return LW2Parser.RULE_separator

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterSeparator" ):
                listener.enterSeparator(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitSeparator" ):
                listener.exitSeparator(self)




    def separator(self):

        localctx = LW2Parser.SeparatorContext(self, self._ctx, self.state)
        self.enterRule(localctx, 16, self.RULE_separator)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 67
            self.match(LW2Parser.T__2)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx





