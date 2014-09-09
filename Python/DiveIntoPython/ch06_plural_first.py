# -*- coding: utf-8 -*-
'''
• If a word ends in S, X, or Z, add ES. Bass becomes basses,
fax becomes faxes, and waltz becomes waltzes.
• If a word ends in a noisy H, add ES; if it ends in a silent H,
just add S. What’s a noisy H? One that gets
combined with other letters to make a sound that you can hear.
So coach becomes coaches and rash
becomes rashes, because you can hear the CH and SH sounds
when you say them. But cheetah becomes
cheetahs, because the H is silent.
• If a word ends in Y that sounds like I, change the Y to IES;
if the Y is combined with a vowel to sound like
something else, just add S. So vacancy becomes vacancies,
but day becomes days.
• If all else fails, just add S and hope for the best.
'''
import re


def plural(noun):
    if re.search('[sxz]$', noun):
        return re.sub('$', 'es', noun)
    elif re.search('[^aeioudgkprt]h$', noun):
        return re.sub('$', 'es', noun)
    elif re.search('[^aeiou]y$', noun):
        return re.sub('y$', 'ies', noun)
    else:
        return noun + 's'
