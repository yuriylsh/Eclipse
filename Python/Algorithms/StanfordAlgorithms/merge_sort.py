def mergesort(arr):
    '''
    Performs merge sort.
    Worst case performance: O(n log n)
    Stable: true
    In-place: false
    '''
    length = len(arr)
    if length <= 1:
        return arr

    middle = length / 2
    left = mergesort(arr[:middle])
    right = mergesort(arr[middle:])
    return mergesort_combine(left, right)


def mergesort_combine(left, right):
    ''' recursive part of merge sort'''
    result = []
    l, l_len = 0, len(left)
    r, r_len = 0, len(right)
    while l < l_len and r < r_len:
        if left[l] <= right[r]:
            result.append(left[l])
            l += 1
        else:
            result.append(right[r])
            r += 1
    if l < l_len:
        result.extend(left[l:])
    if r < r_len:
        result.extend(right[r:])
    return result


if __name__ == '__main__':
    arr = [3, 2, 5, 1, 4]
    print(arr)
    sorted = mergesort(arr)
    print(sorted)
