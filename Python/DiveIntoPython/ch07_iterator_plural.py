from ch06_plural_file_of_patterns import build_match_and_apply_functions


class LazyRules:

    '''Represents an iterator equivalent to the one
    created by generator in ch06_generators_plural.py'''

    def __init__(self):
        self.pattern_file = open(
            'ch06_plural_file_of_patterns.txt', encoding='utf-8')
        self.cache = []

    def __iter__(self):
        self.cache_index = 0
        return self

    def __next__(self):
        self.cache_index += 1
        if len(self.cache) >= self.cache_index:
            return self.cache[self.cache_index - 1]

        if self.pattern_file.closed:
            raise StopIteration

        line = self.pattern_file.readline()
        if not line:
            raise StopIteration

        pattern, search, replace = line.split(None, 3)
        funcs = build_match_and_apply_functions(pattern, search, replace)
        self.cache.append(funcs)
        return funcs

if __name__ == '__main__':
    rules = LazyRules()

    def plural(noun):
        for matches_rule, apply_rule in rules:
            if matches_rule(noun):
                return apply_rule(noun)
        raise ValueError('no matching rule for {0}'.format(noun))
    print(plural('wish'))
    print(plural('Noah'))
    print(plural('victory'))
    print(plural('pay'))
    print(plural('fox'))
    print(plural('word'))
