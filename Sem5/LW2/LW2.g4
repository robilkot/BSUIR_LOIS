grammar LW2;

kb: header implication '\n\n' rule;

header: NAME '\n\n';
implication: elements '\n' truth_degrees;
rule: elements '\n' truth_degrees ('\n' truth_degrees)+;

elements: element (separator* element)+;
truth_degrees: membership_degree (separator* membership_degree)+;

element: NAME;
// todo: only allow float from 0 to 1
membership_degree: FLOAT;
separator: ' ';
NAME: [a-zA-Z] [a-zA-Z0-9]+;
FLOAT: [0-9]+ ('.' [0-9]+)?;

LINE_COMMENT: '#' ~[\r\n]* -> skip;
WS: [ \t\r\n]+ -> skip;
