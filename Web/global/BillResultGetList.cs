using System;
using System.Data;

namespace global
{
    internal class BillResultGetList : DataTable
    {
        private int value1;
        private DateTime fromDate;
        private DateTime toDate;
        private int areaId;
        private int provinceId;
        private int districtId;
        private int townId;
        private object value2;
        private string text1;
        private string text2;
        private string text3;
        private string text4;
        private int pageNumber;
        private int v;

        public BillResultGetList(int value1, DateTime fromDate, DateTime toDate, int areaId, int provinceId, int districtId, int townId, object value2, string text1, string text2, string text3, string text4, int pageNumber, int v)
        {
            this.value1 = value1;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.areaId = areaId;
            this.provinceId = provinceId;
            this.districtId = districtId;
            this.townId = townId;
            this.value2 = value2;
            this.text1 = text1;
            this.text2 = text2;
            this.text3 = text3;
            this.text4 = text4;
            this.pageNumber = pageNumber;
            this.v = v;
        }
    }
}