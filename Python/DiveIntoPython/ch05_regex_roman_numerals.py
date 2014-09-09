# -*- coding: utf-8 -*-
'''
The following are some general rules for constructing Roman numerals:
• Sometimes characters are additive. I is 1 , II is 2 , and III is 3.
VI is 6 (literally, “ 5 and 1 ”), VII is 7 , and
VIII is 8 .
• The tens characters ( I , X , C , and M ) can be repeated up to three times.
At 4 , you need to subtract from the
next highest fives character. You can't represent 4 as IIII ;
instead, it is represented as IV (“ 1 less than 5 ”).
40 is written as XL (“ 10 less than 50 ”), 41 as XLI ,
42 as XLII , 43 as XLIII , and then 44 as XLIV (“ 10 less
than 50 , then 1 less than 5 ”).
• Sometimes characters are… the opposite of additive.
By putting certain characters before others, you
subtract from the final value. For example, at 9 ,
you need to subtract from the next highest tens character: 8
is VIII , but 9 is IX (“ 1 less than 10 ”),
not VIIII (since the I character can not be repeated four times). 90
is XC , 900 is CM .
• The fives characters can not be repeated. 10 is always represented as X ,
never as VV . 100 is always C , never
LL .
• Roman numerals are read left to right, so the order of characters
matters very much. DC is 600 ; CD is a
completely different number ( 400 , “ 100 less than 500 ”). CI is 101 ;
IC is not even a valid Roman numeral
(because you can't subtract 1 directly from 100 ;
you would need to write it as XCIX , “ 10 less than 100 , then
1 less than 10 ”).
'''
import re
pattern = '^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$'
print(re.search(pattern, 'MMMDCCCLXXXVIII'))

pattern = '''
^ 					# beginning of string
M{0,3} 				# thousands - 0 to 3 Ms
(CM|CD|D?C{0,3}) 	# hundreds - 900 (CM), 400 (CD), 0-300 (0 to 3 Cs),
                    # or 500-800 (D, followed by 0 to 3 Cs)
(XC|XL|L?X{0,3}) 	# tens - 90 (XC), 40 (XL), 0-30 (0 to 3 Xs),
                    # or 50-80 (L, followed by 0 to 3 Xs)
(IX|IV|V?I{0,3}) 	# ones - 9 (IX), 4 (IV), 0-3 (0 to 3 Is),
                    # or 5-8 (V, followed by 0 to 3 Is)
$ 					# end of string
'''
print(re.search(pattern, 'MMMDCCCLXXXVIII', re.VERBOSE))
