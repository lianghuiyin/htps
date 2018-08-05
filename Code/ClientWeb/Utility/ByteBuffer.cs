namespace Utility
{
    using System;
    /// <summary>
    /// ����һ���ɱ䳤��Byte���鷽��Push���ݺ�Pop����
    /// �������󳤶�Ϊ1024,������������
    /// �������󳤶��ɳ���MAX_LENGTH�趨
    /// 
    /// ע:����ʵ����Ҫ,����Ҫ������ȡ����,��������
    /// �����Pop�����������Ƚ�����ĺ���,���Ǵ�0��ʼ.
    /// 
    /// @Author: Red_angelX
    /// </summary>
    class ByteBuffer
    {
        //�������󳤶�
        private const int MAX_LENGTH = 1024;

        //�̶����ȵ��м�����
        private byte[] TEMP_BYTE_ARRAY = new byte[MAX_LENGTH];

        //��ǰ���鳤��
        private int CURRENT_LENGTH = 0;

        //��ǰPopָ��λ��
        private int CURRENT_POSITION = 0;

        //��󷵻�����
        private byte[] RETURN_ARRAY;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ByteBuffer()
        {
            this.Initialize();
        }

        /// <summary>
        /// ���صĹ��캯��,��һ��Byte����������
        /// </summary>
        /// <param name="bytes">���ڹ���ByteBuffer������</param>
        public ByteBuffer(byte[] bytes)
        {
            this.Initialize();
            this.PushByteArray(bytes);
        }


        /// <summary>
        /// ��ȡ��ǰByteBuffer�ĳ���
        /// </summary>
        public int Length
        {
            get
            {
                return CURRENT_LENGTH;
            }
        }

        /// <summary>
        /// ��ȡ/���õ�ǰ��ջָ��λ��
        /// </summary>
        public int Position
        {
            get
            {
                return CURRENT_POSITION;
            }
            set
            {
                CURRENT_POSITION = value;
            }
        }

        /// <summary>
        /// ��ȡByteBuffer�����ɵ�����
        /// ���ȱ���С�� [MAXSIZE]
        /// </summary>
        /// <returns>Byte[]</returns>
        public byte[] ToByteArray()
        {
            //�����С
            RETURN_ARRAY = new byte[CURRENT_LENGTH];
            //����ָ��
            Array.Copy(TEMP_BYTE_ARRAY, 0, RETURN_ARRAY, 0, CURRENT_LENGTH);
            return RETURN_ARRAY;
        }

        /// <summary>
        /// ��ʼ��ByteBuffer��ÿһ��Ԫ��,���ѵ�ǰָ��ָ��ͷһλ
        /// </summary>
        public void Initialize()
        {
            TEMP_BYTE_ARRAY.Initialize();
            CURRENT_LENGTH = 0;
            CURRENT_POSITION = 0;
        }

        /// <summary>
        /// ��ByteBufferѹ��һ���ֽ�
        /// </summary>
        /// <param name="by">һλ�ֽ�</param>
        public void PushByte(byte by)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = by;
        }

        /// <summary>
        /// ��ByteBufferѹ������
        /// </summary>
        /// <param name="ByteArray">����</param>
        public void PushByteArray(byte[] ByteArray)
        {
            //���Լ�CopyToĿ������
            ByteArray.CopyTo(TEMP_BYTE_ARRAY, CURRENT_LENGTH);
            //��������
            CURRENT_LENGTH += ByteArray.Length;
        }

        /// <summary>
        /// ��ByteBufferѹ�����ֽڵ�Short
        /// </summary>
        /// <param name="Num">2�ֽ�Short</param>
        public void PushUInt16(UInt16 Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x00ff) & 0xff);
        }

        /// <summary>
        /// ��ByteBufferѹ��һ���޷�Intֵ
        /// </summary>
        /// <param name="Num">4�ֽ�UInt32</param>
        public void PushInt(UInt32 Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff000000) >> 24) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x00ff0000) >> 16) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x0000ff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x000000ff) & 0xff);
        }

        /// <summary>
        /// ��ByteBufferѹ��һ��Longֵ
        /// </summary>
        /// <param name="Num">4�ֽ�Long</param>
        public void PushLong(long Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff000000) >> 24) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x00ff0000) >> 16) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x0000ff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x000000ff) & 0xff);
        }

        /// <summary>
        /// ��ByteBuffer�ĵ�ǰλ�õ���һ��Byte,������һλ
        /// </summary>
        /// <returns>1�ֽ�Byte</returns>
        public byte PopByte()
        {
            byte ret = TEMP_BYTE_ARRAY[CURRENT_POSITION++];
            return ret;
        }

        /// <summary>
        /// ��ByteBuffer�ĵ�ǰλ�õ���һ��Short,��������λ
        /// </summary>
        /// <returns>2�ֽ�Short</returns>
        public UInt16 PopUInt16()
        {
            //���
            if (CURRENT_POSITION + 1 >= CURRENT_LENGTH)
            {
                return 0;
            }
            UInt16 ret = (UInt16)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1]);
            CURRENT_POSITION += 2;
            return ret;
        }

        /// <summary>
        /// ��ByteBuffer�ĵ�ǰλ�õ���һ��uint,������4λ
        /// </summary>
        /// <returns>4�ֽ�UInt</returns>
        public uint PopUInt()
        {
            if (CURRENT_POSITION + 3 >= CURRENT_LENGTH)
                return 0;
            uint ret = (uint)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 24 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1] << 16 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 2] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 3]);
            CURRENT_POSITION += 4;
            return ret;
        }

        /// <summary>
        /// ��ByteBuffer�ĵ�ǰλ�õ���һ��long,������4λ
        /// </summary>
        /// <returns>4�ֽ�Long</returns>
        public long PopLong()
        {
            if (CURRENT_POSITION + 3 >= CURRENT_LENGTH)
                return 0;
            long ret = (long)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 24 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1] << 16 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 2] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 3]);
            CURRENT_POSITION += 4;
            return ret;
        }

        /// <summary>
        /// ��ByteBuffer�ĵ�ǰλ�õ�������ΪLength��Byte����,����Lengthλ
        /// </summary>
        /// <param name="Length">���鳤��</param>
        /// <returns>Length���ȵ�byte����</returns>
        public byte[] PopByteArray(int Length)
        {
            //���
            if (CURRENT_POSITION + Length >= CURRENT_LENGTH)
            {
                return new byte[0];
            }
            byte[] ret = new byte[Length];
            Array.Copy(TEMP_BYTE_ARRAY, CURRENT_POSITION, ret, 0, Length);
            //����λ��
            CURRENT_POSITION += Length;
            return ret;
        }

    }
}

