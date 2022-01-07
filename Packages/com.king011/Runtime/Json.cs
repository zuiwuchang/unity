using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.king011
{
    static class JsonSmall
    {
        public class DecoderException : Exception
        {
            public DecoderException(string message) : base(message) { }
        }
        public class Decoder
        {
            string _str;
            int _offset = 0;
            public Decoder(string str)
            {
                _str = str;
            }
            private bool peek(char c, bool end = false)
            {
                for (; _offset < _str.Length; _offset++)
                {
                    char ch = _str[_offset];
                    if (isSpace(ch))
                    {
                        continue;
                    }
                    else if (ch == c)
                    {
                        _offset++;
                        return false;
                    }
                    else if (end && ch == '}')
                    {
                        _offset++;
                        return true;
                    }
                    break;
                }
                throw new DecoderException($"not found delim('{c}')");
            }
            private bool peekNext(char c)
            {
                bool delim = false;
                for (; _offset < _str.Length; _offset++)
                {
                    char ch = _str[_offset];
                    if (isSpace(ch))
                    {
                        continue;
                    }
                    else if (ch == c)
                    {
                        _offset++;
                        return false;
                    }
                    else if (delim)
                    {
                        break;
                    }
                    else if (ch == '}')
                    {
                        _offset++;
                        return true;
                    }
                    else if (ch == ',')
                    {
                        delim = true;
                        continue;
                    }
                    break;
                }
                throw new DecoderException($"not found delim('{c}')");
            }
            public void Start()
            {
                peek('{');
            }
            private bool isSpace(char c)
            {
                return c <= ' ' && (c == ' ' || c == '\t' || c == '\r' || c == '\n');
            }
            private bool first = true;
            public string Key()
            {
                bool end;
                if (first)
                {
                    first = false;
                    end = peek('"', true);
                }
                else
                {
                    end = peekNext('"');
                }
                if (end)
                {
                    return null;
                }
                return Decode();
            }
            public string Value()
            {
                peek(':');
                peek('"');
                return Decode();
            }
            private System.Text.StringBuilder builder = new System.Text.StringBuilder();
            private string Decode()
            {
                builder.Clear();
                int start = _offset;
                _offset++;
                char c;
                var escape = false;
                string str;
                int length;
                for (; _offset < _str.Length; _offset++)
                {
                    c = _str[_offset];
                    if (escape)
                    {
                        if (c == 'u')
                        {
                            // 4 個十六進制數字
                            throw new DecoderException($"Unsupported escaping '\\u'");
                        }
                        else
                        {
                            switch (c)
                            {
                                case '\\':
                                    str = "\\";
                                    break;
                                case '"':
                                    str = "\"";
                                    break;
                                case '\'':
                                    str = "'";
                                    break;
                                case '/':
                                    str = "/";
                                    break;
                                case 'b':
                                    str = "\b";
                                    break;
                                case 'f':
                                    str = "\f";
                                    break;
                                case 't':
                                    str = "\t";
                                    break;
                                case 'r':
                                    str = "\r";
                                    break;
                                case 'n':
                                    str = "\n";
                                    break;
                                default:
                                    throw new DecoderException($"Unsupported escaping '\\{c}'");
                            }
                            start = _offset + 1;
                            builder.Append(str);
                        }
                        escape = false;
                        continue;
                    }

                    switch (c)
                    {
                        case '"':
                            length = _offset - start;
                            if (length == 0)
                            {
                                str = "";
                            }
                            else
                            {
                                str = _str.Substring(start, length);
                            }
                            if (builder.Length != 0)
                            {
                                str = builder.Append(str).ToString();
                            }
                            _offset++;
                            return str;
                        case '\\':
                            escape = true;
                            length = _offset - start;
                            if (length == 0)
                            {
                                str = "";
                            }
                            else
                            {
                                str = _str.Substring(start, length);
                                builder.Append(str);
                            }
                            break;
                    }
                }
                throw new DecoderException($"Decode value not found end delim('\"')");
            }
        }
        static public Dictionary<string, string> MapString(string str)
        {
            var map = new Dictionary<string, string>();
            var dec = new Decoder(str);
            dec.Start();
            string key, value;
            while (true)
            {
                key = dec.Key();
                if (key == null)
                {
                    break;
                }
                value = dec.Value();
                map.Add(key, value);
            }
            return map;
        }
    }
}