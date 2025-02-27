﻿// Copyright 2021 Yubico AB
//
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Yubico.Core.Iso7816;

namespace Yubico.YubiKey.Management.Commands
{
    /// <summary>
    /// The response to the <see cref="GetThalesSerialNumberCommand"/> command, containing the YubiKey's
    /// device configuration details.
    /// </summary>
    public class GetThalesSerialNumberResponse : YubiKeyResponse, IYubiKeyResponseWithData<string>
    {
        private const int expectedResponseLength = 11;

        /// <summary>
        /// Constructs a GetPagedDeviceInfoResponse instance based on a ResponseApdu received from the YubiKey.
        /// </summary>
        /// <param name="responseApdu">
        /// The ResponseApdu returned by the YubiKey.
        /// </param>
        public GetThalesSerialNumberResponse(ResponseApdu responseApdu)
            : base(responseApdu)
        {

        }

        /// <summary>
        /// Gets the <see cref="YubiKeyDeviceInfo"/> class that contains details about the current
        /// configuration of the YubiKey.
        /// </summary>
        /// <returns>
        /// The data in the response APDU, presented as a YubiKeyDeviceInfo class.
        /// </returns>
        public string GetData()
        {
            if (ResponseApdu.SW != SWConstants.Success)
            {
                throw new InvalidOperationException(ExceptionMessages.NoResponseDataApduFailed);
            }

            if (ResponseApdu.Data.Length != expectedResponseLength)
            {
                throw new MalformedYubiKeyResponseException(ExceptionMessages.UnknownFidoError);
            }

            byte[] serialNumberBytes = MemoryMarshal.AsBytes(ResponseApdu.Data.Span.Slice(3)).ToArray();
            return System.Text.Encoding.Default.GetString(serialNumberBytes);

        }
    }
}
