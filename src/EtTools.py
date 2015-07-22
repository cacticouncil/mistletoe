import random
import string

def getRandomPassword(rangeLow = 7, rangeHigh = 16):
    size = int(round(random.SystemRandom().uniform(rangeLow, rangeHigh)))
    return ''.join(random.SystemRandom().choice(string.uppercase + string.lowercase + string.digits) for _ in xrange(size))

def transposeData(dataset):
    return [list(i) for i in zip(*dataset)]