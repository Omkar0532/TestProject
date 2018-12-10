class Data_Transfer():
    def __init__(self):
        self.p1 = ''
        self.p2 = ''
        self.p3 = ''
        self.p4 = ''
        self.p5 = ''

    def getForm_data(self, s1, s2, s3, s4, s5):
        self.p1 = s1
        self.p2 = s2
        self.p3 = s3
        self.p4 = s4
        self.p5 = s5
        # print self.p1, self.p2, self.p3, self.p4, self.p5+'5555555555'
        return self.p1, self.p2, self.p3, self.p4, self.p5