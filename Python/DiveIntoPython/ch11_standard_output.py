import sys


class RedirectStdoutTo:

    def __init__(self, out_new):
        self.out_new = out_new

    def __enter__(self):
        self.out_old = sys.stdout
        sys.stdout = self.out_new

    def __exit__(self, *args):
        sys.stdout = self.out_old

if __name__ == '__main__':
    print('A')
    with open('examples/out.log', mode='w', encoding='utf-8') as a_file, RedirectStdoutTo(a_file):
        '''equivalent to
                        with open(...) as a_file
                                with RedirectStdoutTo(a_file)'''
        print('B')
    print('C')
